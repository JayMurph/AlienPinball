/**
 * FILE             : HighScoresDisplay.cs
 * PROJECT          : SENG3060-A1
 * PROGRAMMER       : Joshua Murphy
 * FIRST VERSION    : February 1, 2023
 * DESCRIPTION      : Contains the HighScoresDisplay class
 */
using TMPro;
using UnityEngine;

/// <summary>
/// Retrieves and formats the high scores of the pinball game then applies them
/// to a text field
/// </summary>
[RequireComponent(typeof(HighScoresHelper))]
public class HighScoresDisplay : MonoBehaviour
{
    /// <summary>
    /// alignment and field width for player initials text
    /// </summary>
    private const int PLAYER_INITIALS_TEXT_ALIGNMENT = -4;

    /// <summary>
    /// alignment and field width for score value text
    /// </summary>
    private const int SCORE_TEXT_ALIGNMENT = 6;

    /// <summary>
    /// To have score text displayed in it
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI text;

    /// <summary>
    /// For retrieving current high scores
    /// </summary>
    private HighScoresHelper helper;

    /// <summary>
    /// Retrieves, formats, then displays the high scores of the pinball game
    /// </summary>
    void Start()
    {
        helper = GetComponent<HighScoresHelper>();
        helper.GetHighScores()
            .ForEach(t => text.text += $"{t.Item1, PLAYER_INITIALS_TEXT_ALIGNMENT} : {t.Item2, SCORE_TEXT_ALIGNMENT}\n");
    }
}
