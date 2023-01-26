using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreOnBallCollision : MonoBehaviour
{
    public int ScoreValue;
    public float TimeoutSecs;

    [SerializeField]
    private MessageChannelScriptableObject messageChannel;

    [SerializeField]
    private string ballTag;

    private float lastScoreTime = 0;

    void OnCollisionEnter(Collision collision)
    {
        if (
            collision.gameObject.tag == ballTag && 
            (lastScoreTime == 0 || lastScoreTime < Time.time + TimeoutSecs)
        )
        {
            lastScoreTime = Time.time;
            messageChannel.Score.Invoke(ScoreValue);
        }
    }
}
