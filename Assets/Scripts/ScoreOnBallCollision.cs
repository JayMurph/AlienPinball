using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreOnBallCollision : MonoBehaviour
{
    public int ScoreValue;
    public float TimeoutSecs;

    [SerializeField]
    private Light l = null;

    [SerializeField]
    private MessageChannelScriptableObject messageChannel;

    [SerializeField]
    private string ballTag;

    private float lastScoreTime = 0;

    private void Start()
    {
        if (l != null)
        {
            l.enabled = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (
            collision.gameObject.tag == ballTag && 
            (lastScoreTime == 0 || lastScoreTime < Time.time + TimeoutSecs)
        )
        {
            lastScoreTime = Time.time;
            messageChannel.Score.Invoke(ScoreValue);
            StartCoroutine(RunLight());
        }
    }

    IEnumerator RunLight()
    {
        if ( l != null)
        {
            l.enabled = true;
            yield return new WaitForSeconds(TimeoutSecs);
            l.enabled = false;
        }
        else
        {
            yield return null;
        }
    }
}
