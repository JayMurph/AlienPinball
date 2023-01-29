using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PinballGameController : MonoBehaviour
{
    public UnityEvent GameOver;

    public int StartingBalls = 3;

    [SerializeField]
    private AudioSource backgroundMusic;
    [SerializeField]
    private AudioSource ballDeathSound;
    [SerializeField]
    private AudioSource bellSound;
    [SerializeField]
    private AudioSource gameOverSound;
    [SerializeField]
    BallController ball;

    [SerializeField]
    private MessageChannelScriptableObject scoreEventChannel;

    [SerializeField]
    private Transform ballSpawnTransform;

    [SerializeField]
    private TextMeshProUGUI ballsRemainingValueText;

    [SerializeField]
    private TextMeshProUGUI scoreValueText;

    [SerializeField]
    TextMeshProUGUI finalScoreValueText;

    private int balls;
    private int score;

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

    private void OnDestroy()
    {
        scoreEventChannel.Event.RemoveListener(OnScore); 
    }

    public void OnBallDeath()
    {
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
            balls--;
            ballsRemainingValueText.text = balls.ToString();
            ball.transform.position = ballSpawnTransform.position;
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
