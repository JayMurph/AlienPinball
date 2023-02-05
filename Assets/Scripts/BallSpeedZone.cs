/**
 * FILE             : BallSpeedZone.cs
 * PROJECT          : SENG3060-A1
 * PROGRAMMER       : Joshua Murphy
 * FIRST VERSION    : February 1, 2023
 * DESCRIPTION      : Contains the BallSpeedZone class
 */
using System.Collections;
using UnityEngine;

/// <summary>
/// Detects if the pinball has been within its bounds for a given period of
/// time then, if so, applies a force to it in a specific direction
/// </summary>
[RequireComponent(typeof(Collider))]
public class BallSpeedZone : MonoBehaviour
{
    /// <summary>
    /// Direction to apply force on the pinball
    /// </summary>
    public Vector3 Direction = Vector3.forward;
    public float Force;
    /// <summary>
    /// Amount of time that the pinball must be within the object's bounds
    /// before force is applied to it
    /// </summary>
    public float DelayBeforeApplyingForceSecs;

    /// <summary>
    /// For identifying the pinball during collisions
    /// </summary>
    [SerializeField]
    private string ballTag;

    private Coroutine applyForceAfterDelayCoroutine = null;

    /// <summary>
    /// Draw a line representing the direction of force that the BallSpeedZone
    /// will apply
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Direction * Force); 
    }

    /// <summary>
    /// When colliding with the pinball, starts a coroutine to apply force to the ball after a delay
    /// </summary>
    /// <param name="other">The colliding object</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(ballTag))
        {
            applyForceAfterDelayCoroutine = 
                StartCoroutine(ApplyForceAfterDelay(other.gameObject.GetComponent<BallController>(), DelayBeforeApplyingForceSecs));
        }
    }

    /// <summary>
    /// When colliding with the pinball, stops the coroutine set to apply force to it
    /// </summary>
    /// <param name="other">The exiting object</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(ballTag))
        {
            StopCoroutine(applyForceAfterDelayCoroutine);
        }
    }

    /// <summary>
    /// Applies force to the pinball after a given amount of time
    /// </summary>
    /// <param name="ball">To have force applied to it after a delay</param>
    /// <param name="delaySecs">Delay before applying force to the ball</param>
    /// <returns>How long to wait before returning to this method</returns>
    private IEnumerator ApplyForceAfterDelay(BallController ball, float delaySecs)
    {
        if (ball != null)
        {
            yield return new WaitForSeconds(delaySecs);
            ball.AddForce(Direction * Force); 
        } 
    }
}
