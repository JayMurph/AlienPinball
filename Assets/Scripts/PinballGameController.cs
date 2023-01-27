using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PinballGameController : MonoBehaviour
{
    public UnityEvent GameOver;
    public UnityEvent DeadBall;

    public int StartingBalls = 3;

    [SerializeField]
    AudioSource backgroundMusic;
    [SerializeField]
    AudioSource ballDeathSound;
    [SerializeField]
    AudioSource bellSound;
    [SerializeField]
    AudioSource gameOverSound;

    [SerializeField]
    MessageChannelScriptableObject scoreEventChannel;

    [SerializeField]
    Transform ballSpawnTransform;

    [SerializeField]
    GameObject ballPrefab;

    [SerializeField]
    TextMeshProUGUI ballsRemainingValueText;

    [SerializeField]
    TextMeshProUGUI scoreValueText;

    [SerializeField]
    TextMeshProUGUI finalScoreValueText;

    int balls;
    int score;

    // Start is called before the first frame update
    void Start()
    {
        balls = StartingBalls - 1;
        ballsRemainingValueText.text = balls.ToString();

        scoreEventChannel.Event.AddListener(OnScore); 
    }

    private void OnScore(int amt)
    {
        score += amt;
        scoreValueText.text = score.ToString();
        bellSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnDestroy()
    {
        scoreEventChannel.Event.RemoveListener(OnScore); 
    }

    public void OnBallDeath()
    {
        Debug.Log("BallDeath");
        ballDeathSound.Play();
        if (balls == 0)
        {
            backgroundMusic.Stop();
            gameOverSound.Play();
            finalScoreValueText.text = score.ToString();
            GameOver.Invoke();
        }
        else
        {
            DeadBall.Invoke();
            balls--;
            ballsRemainingValueText.text = balls.ToString();
            var newBall = Instantiate(ballPrefab, ballSpawnTransform.parent);
            newBall.transform.position = ballSpawnTransform.position;
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
