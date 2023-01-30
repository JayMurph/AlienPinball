using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoresHelper : MonoBehaviour
{
    [SerializeField]
    private string highScoresKey = "highscores"; 

    private List<Tuple<string, int>> highScores = new List<Tuple<string, int>>();

    private readonly int MAX_HIGH_SCORES = 5;

    // Start is called before the first frame update
    void Awake()
    {
        if (PlayerPrefs.HasKey(highScoresKey))
        {
            highScores = JsonConvert.DeserializeObject<List<Tuple<string, int>>>(PlayerPrefs.GetString(highScoresKey));
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
        return highScores;
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
            highScores = highScores.OrderBy(t => t.Item2).ToList();
            return true;
        }
        else if (highScores.Any(t => t.Item2 < score))
        {
            highScores.RemoveAt(highScores.Count - 1);
            highScores.Insert(highScores.FindIndex(t => t.Item2 < score), new Tuple<string, int>(name, score));
            return true;
        }

        return false;
    }
}
