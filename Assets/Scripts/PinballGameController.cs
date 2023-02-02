/**
 * FILE             : PinballGameController.cs
 * PROJECT          : SENG3060-A1
 * PROGRAMMER       : Joshua Murphy
 * FIRST VERSION    : February 1, 2023
 * DESCRIPTION      : Contains the PinballGameController class
 */
using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Encapsulates the logic of a pinbal game machine. Triggers sound, track's
/// the player's score, determines when the game is over
/// </summary>
public class PinballGameController : MonoBehaviour
{
    /// <summary>
    /// Invoked when the player has run of balls to play with. The player's final score is passed as a parameter
    /// </summary>
    public UnityEvent<int> GameOver;

    /// <summary>
    /// Amount of balls available to the player during a play session
    /// </summary>
    public int StartingBalls = 3;

    [SerializeField]
    private BallController ball;

    [SerializeField]
    private AudioSource backgroundMusic;
    [SerializeField]
    private AudioSource ballDeathSound;
    [SerializeField]
    private AudioSource bellSound;
    [SerializeField]
    private AudioSource gameOverSound;

    /// <summary>
    /// For receiving score updates
    /// </summary>
    [SerializeField]
    private IntMessageChannelScriptableObject scoreEventChannel;

    /// <summary>
    /// Text to display player's remaining balls 
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI ballsRemainingValueText;

    /// <summary>
    /// Text to display player's current score
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI scoreValueText;

    private int ballsRemaining;
    private int currentScore;

    /// <summary>
    /// Display balls remaining and subscribe to scoring events
    /// </summary>
    void Start()
    {
        ballsRemaining = StartingBalls - 1;
        ballsRemainingValueText.text = ballsRemaining.ToString();

        scoreEventChannel.Event.AddListener(OnScore); 
    }

    /// <summary>
    /// Callback for scoring events. Adds the given amount to the player's
    /// current score and updates the display
    /// </summary>
    /// <param name="scoreIncrement">Amount to add to player's score</param>
    private void OnScore(int scoreIncrement)
    {
        currentScore += scoreIncrement;
        scoreValueText.text = currentScore.ToString();
        bellSound.Play();
    }

    /// <summary>
    /// Unsubscribes from scoring events
    /// </summary>
    private void OnDestroy()
    {
        scoreEventChannel.Event.RemoveListener(OnScore); 
    }

    /// <summary>
    /// Callback for ball death event. Either spawns a new ball or ends the
    /// game.
    /// </summary>
    public void OnBallDeath()
    {
        ballDeathSound.Play();

        if (ballsRemaining == 0)
        {
            // End game
            backgroundMusic.Stop();
            gameOverSound.Play();
            GameOver.Invoke(currentScore);
        }
        else
        {
            // spawn a new ball
            ballsRemaining--;
            ballsRemainingValueText.text = ballsRemaining.ToString();
            ball.Respawn();
        }
    }
}
