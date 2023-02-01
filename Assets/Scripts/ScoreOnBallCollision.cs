/**
 * FILE             : ScoreOnBallCollision.cs
 * PROJECT          : SENG3060-A1
 * PROGRAMMER       : Joshua Murphy
 * FIRST VERSION    : February 1, 2023
 * DESCRIPTION      : Contains the ScoreOnBallCollisionClass
 */
using System.Collections;
using UnityEngine;

/// <summary>
/// Encapsulates behaviour for an object that adds to the player's score when
/// collided with by a pinball
/// </summary>
public class ScoreOnBallCollision : MonoBehaviour
{
    /// <summary>
    /// Score value that the object gives upon collision
    /// </summary>
    public int ScoreValue;

    /// <summary>
    /// Minimum amount of time before the player can score again on this object
    /// </summary>
    public float TimeoutSecs;

    /// <summary>
    /// To be activated when the object is scored upon
    /// </summary>
    [SerializeField]
    private Light scoreLight = null;

    /// <summary>
    /// For publishing when this object is scored on
    /// </summary>
    [SerializeField]
    private IntMessageChannelScriptableObject scoreChannel;

    [SerializeField]
    private string ballTag;

    /// <summary>
    /// Game-time at which the player last scored on this object
    /// </summary>
    private float lastScoreTimeSecs = 0; 

    /// <summary>
    /// Disabled the object's light if found
    /// </summary>
    private void Start()
    {
        if (scoreLight != null)
        {
            scoreLight.enabled = false;
        }
    }

    /// <summary>
    /// Publishes a score event and turns on the object's light
    /// </summary>
    /// <param name="collision">Information about the collision</param>
    void OnCollisionEnter(Collision collision)
    {
        //If we collided with ball, and enough time has elapsed since the last collision
        if (collision.gameObject.tag == ballTag && 
            (lastScoreTimeSecs == 0 || lastScoreTimeSecs < Time.time + TimeoutSecs))
        {
            lastScoreTimeSecs = Time.time;
            // publish score event
            scoreChannel.Event.Invoke(ScoreValue);
            // turn on object's light until player can score again
            StartCoroutine(ShowScoreLight(TimeoutSecs));
        }
    }

    /// <summary>
    /// Enable's the object's light for a given amount of time
    /// </summary>
    /// <param name="durationSecs">How long to enable the object's light for</param>
    /// <returns>How long to wait until the next call of the routine</returns>
    IEnumerator ShowScoreLight(float durationSecs)
    {
        if (scoreLight != null)
        {
            scoreLight.enabled = true;
            yield return new WaitForSeconds(durationSecs);
            scoreLight.enabled = false;
        }
        else
        {
            yield return null;
        }
    }
}
