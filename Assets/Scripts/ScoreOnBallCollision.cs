using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreOnBallCollision : MonoBehaviour
{
    public int ScoreValue;
    public float TimeoutSecs;

    [SerializeField]
    private Light scoreLight = null;

    [SerializeField]
    private IntMessageChannelScriptableObject scoreChannel;

    [SerializeField]
    private string ballTag;

    private float lastScoreTime = 0;

    private void Start()
    {
        if (scoreLight != null)
        {
            scoreLight.enabled = false;
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
            scoreChannel.Event.Invoke(ScoreValue);
            StartCoroutine(RunLight());
        }
    }

    IEnumerator RunLight()
    {
        if (scoreLight != null)
        {
            scoreLight.enabled = true;
            yield return new WaitForSeconds(TimeoutSecs);
            scoreLight.enabled = false;
        }
        else
        {
            yield return null;
        }
    }
}
