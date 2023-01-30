using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(HighScoresHelper))]
public class HighScoresController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    private HighScoresHelper helper;

    // Start is called before the first frame update
    void Start()
    {
        helper = GetComponent<HighScoresHelper>();
        helper.GetHighScores().ForEach(t => text.text += $"{t.Item1,3} - {t.Item2,-6}\n");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
