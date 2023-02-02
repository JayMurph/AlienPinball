/**
 * FILE             : HighScoresHelper.cs
 * PROJECT          : SENG3060-A1
 * PROGRAMMER       : Joshua Murphy
 * FIRST VERSION    : February 1, 2023
 * DESCRIPTION      : Contains the HighScoresHelper class
 */
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages and provides access to the high scores of the pinball game
/// </summary>
public class HighScoresHelper : MonoBehaviour
{
    /// <summary>
    /// Key value to use when saving high scores to player prefs
    /// </summary>
    [SerializeField]
    private string highScoresKey = "highscores";

    /// <summary>
    /// high scores. Tuples containsstring with user's initials and their associated high score
    /// </summary>
    private List<Tuple<string, int>> highScores = new List<Tuple<string, int>>(); 

    /// <summary>
    /// Max number of high scores to save 
    /// </summary>
    private const int MAX_HIGH_SCORES = 5;

    // Start is called before the first frame update
    void Awake()
    {
        if (PlayerPrefs.HasKey(highScoresKey))
        {
            highScores = JsonConvert.DeserializeObject<List<Tuple<string, int>>>(PlayerPrefs.GetString(highScoresKey));
            highScores = highScores.OrderByDescending(x => x.Item2).ToList();
        }
        else
        {
            PlayerPrefs.SetString(highScoresKey, JsonConvert.SerializeObject(highScores));
        }
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetString(highScoresKey, JsonConvert.SerializeObject(highScores));
        PlayerPrefs.Save();
    }

    public List<Tuple<string, int>> GetHighScores()
    {
        return highScores.OrderByDescending(t => t.Item2).ToList();
    }

    public bool IsNewHighScore(int score)
    {
        return highScores.Any(t => t.Item2 < score) || highScores.Count < MAX_HIGH_SCORES;
    }

    public bool TrySubmitHighScore(string name, int score)
    {
        if (highScores.Count < MAX_HIGH_SCORES)
        {
            highScores.Add(new Tuple<string, int>(name, score));
            highScores = highScores.OrderByDescending(t => t.Item2).ToList();
            return true;
        }
        else if (highScores.Any(t => t.Item2 < score))
        {
            highScores = highScores.OrderByDescending(t => t.Item2).ToList();
            highScores.Insert(highScores.FindIndex(t => t.Item2 < score), new Tuple<string, int>(name, score));
            highScores.RemoveAt(highScores.Count - 1);
            return true;
        }

        return false;
    }
}
