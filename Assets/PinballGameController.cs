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
    Transform ballSpawnTransform;

    [SerializeField]
    GameObject ballPrefab;

    [SerializeField]
    TextMeshProUGUI ballsRemainingValueText;

    [SerializeField]
    TextMeshProUGUI pointsValueText;

    int balls;
    int score;

    // Start is called before the first frame update
    void Start()
    {
        balls = StartingBalls - 1;
        ballsRemainingValueText.text = balls.ToString();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnBallDeath(BallController ball)
    {
        if (balls == 0)
        {
            GameOver.Invoke();
        }
        else
        {
            balls--;
            ballsRemainingValueText.text = balls.ToString();
            Destroy(ball.gameObject);
            var newBall = Instantiate(ballPrefab, ballSpawnTransform.parent);
            newBall.transform.position = ballSpawnTransform.position;
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
