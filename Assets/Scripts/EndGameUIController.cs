using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(HighScoresHelper))]
public class EndGameUIController : MonoBehaviour
{
    [SerializeField]
    GameObject gameOverPanel;
    [SerializeField]
    GameObject newHighScorePanel;
    [SerializeField]
    List<TextMeshProUGUI> scoreTexts = new List<TextMeshProUGUI>();

    private HighScoresHelper helper;
    private int score;
    private string userInitialsInput;


    // Start is called before the first frame update
    void Start()
    {
        helper = GetComponent<HighScoresHelper>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEndGame(int finalScore)
    {
        this.score = finalScore;

        scoreTexts.ForEach(t => t.text = score.ToString());

        if (helper.IsNewHighScore(score))
        {
            newHighScorePanel.SetActive(true);
        }
        else
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void TrySubmitHighScore()
    {
        if (!string.IsNullOrWhiteSpace(userInitialsInput) && helper.IsNewHighScore(score))
        {
            helper.TrySubmitHighScore(userInitialsInput, score);
        }
    }


    public void HighScoreUserInitialsChanged(string newVal)
    {
        userInitialsInput = newVal;
    }
}
