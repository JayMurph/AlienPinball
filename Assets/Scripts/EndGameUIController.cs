/**
 * FILE             : EndGameUIController.cs
 * PROJECT          : SENG3060-A1
 * PROGRAMMER       : Joshua Murphy
 * FIRST VERSION    : February 1, 2023
 * DESCRIPTION      : Contains the EndGameUIController class
 */
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Contains methods for manipulating the menu displayed at the end of the
/// pinball game (after the player runs out of balls)
/// </summary>
[RequireComponent(typeof(HighScoresHelper))]
public class EndGameUIController : MonoBehaviour
{
    /// <summary>
    /// Generic game over panel for displaying final score
    /// </summary>
    [SerializeField]
    GameObject gameOverPanel;

    /// <summary>
    /// Panel to display when user achieves a new high score
    /// </summary>
    [SerializeField]
    GameObject newHighScorePanel;

    /// <summary>
    /// Text elements to apply the user's final score to
    /// </summary>
    [SerializeField]
    List<TextMeshProUGUI> scoreTexts = new List<TextMeshProUGUI>();

    /// <summary>
    /// for checking if the player achieved a new high score, and possibly saving it
    /// </summary>
    private HighScoresHelper helper;

    private int finalScore;
    private string userInitialsInput;


    /// <summary>
    /// Initializes HighScoresHelper field
    /// </summary>
    void Start()
    {
        helper = GetComponent<HighScoresHelper>();
    }

    /// <summary>
    /// To be called when the player runs out of pinballs and the game ends. If
    /// the player achieves a new high score, displays a menu for submitting
    /// initials and saving the score, otherwise displays the final score in a
    /// generic menu
    /// </summary>
    /// <param name="finalScore">The player's final score</param>
    public void OnEndGame(int finalScore)
    {
        this.finalScore = finalScore;

        // display final score on necessary text elements
        scoreTexts.ForEach(t => t.text = this.finalScore.ToString());

        /*
         * if player achieved a new high score, show panel for entering and
         * submitting initials, otherwise show generic panel
         */
        if (helper.IsNewHighScore(this.finalScore))
        {
            newHighScorePanel.SetActive(true);
        }
        else
        {
            gameOverPanel.SetActive(true);
        }
    }

    /// <summary>
    /// Switches to the pinball games Main Menu Scene
    /// </summary>
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    /// <summary>
    /// Tries to submit the users final score as a new high score
    /// </summary>
    public void TrySubmitHighScore()
    {
        if (!string.IsNullOrWhiteSpace(userInitialsInput) && helper.IsNewHighScore(finalScore))
        {
            helper.TrySubmitHighScore(userInitialsInput, finalScore);
        }
    }

    /// <summary>
    /// Callback for getting the user's input when writing their initials for a new high score
    /// </summary>
    /// <param name="newInput">The most up to date input from the user</param>
    public void HighScoreUserInitialsChanged(string newInput)
    {
        userInitialsInput = newInput;
    }
}
