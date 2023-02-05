/**
 * FILE             : BallController.cs
 * PROJECT          : SENG3060-A1
 * PROGRAMMER       : Joshua Murphy
 * FIRST VERSION    : February 1, 2023
 * DESCRIPTION      : Contains the BallController class
 */
using System.Collections;
using UnityEngine;

/// <summary>
/// Emulates a pinball. Detects if it is stuck stationary on the playfield and
/// will move itself in that case.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class BallController : MonoBehaviour
{
    /// <summary>
    /// Max amount of time to allow the pinball to be stationary on the
    /// playfield before applying a bump force
    /// </summary>
    public float MaxSleepTimeSecs;

    /// <summary>
    /// Force to apply to the pinball when it has been stationary for too long
    /// </summary>
    public float SleepBumpForce;

    /// <summary>
    /// Collider for the playfield of the pinball game. This is the area in
    /// which, if the pinball stays stationary too long, the pinball will be
    /// bumped
    /// </summary>
    [SerializeField]
    private Collider activeGroundTriggerCollider;

    /// <summary>
    /// Point at which to respawn pinball after death
    /// </summary>
    [SerializeField]
    private Transform respawnPoint;

    Rigidbody body;
    Collider col;

    /// <summary>
    /// Tracks if the pinball is on the play field
    /// </summary>
    private bool onActiveGround = false;

    /// <summary>
    /// Coroutine for applying force to pinball, after delay, if it has been
    /// stationary too long
    /// </summary>
    private Coroutine moveSleepingBodyAfterDelayCoroutine;

    /// <summary>
    /// Increments for sweeping the area of the pinball to determine if it is
    /// stuck
    /// </summary>
    private const float SWEEP_ANGLE_INCREMENTS = 30f;

    /// <summary>
    /// Moves the pinball back to the respawn point
    /// </summary>
    public void Respawn()
    {
        transform.position = respawnPoint.position;
    }

    /// <summary>
    /// Initializes the body and col fields
    /// </summary>
    void Start()
    {
        body = GetComponent<Rigidbody>();        
        col = GetComponent<Collider>();
    }

    /// <summary>
    /// Adds a force to the pinball
    /// </summary>
    /// <param name="force">Force to apply to the pinball</param>
    public void AddImpulse(Vector3 force)
    {
        body.AddForce(force, ForceMode.Impulse);
    }

    public void AddForce(Vector3 force)
    {
        body.AddForce(force);
    }

    /// <summary>
    /// Initializes a Coroutine to move the pinball (after a delay) if it has
    /// been stationary on the playfield for too long
    /// </summary>
    private void Update()
    {
        /*
         * If pinball is stationary on playfield, start coroutine to bump it,
         * otherwise stop any active bump coroutines
         */
        if (onActiveGround && body.IsSleeping()) 
        {
            if (moveSleepingBodyAfterDelayCoroutine == null)
            {
                moveSleepingBodyAfterDelayCoroutine = StartCoroutine(MoveSleepingBodyAfterDelay(MaxSleepTimeSecs));
            }
        }
        else if (moveSleepingBodyAfterDelayCoroutine != null)
        {
            StopCoroutine(moveSleepingBodyAfterDelayCoroutine);
            moveSleepingBodyAfterDelayCoroutine = null;
        }
    }

    /// <summary>
    /// Moves the pinball by applying a force to it, or respawning it, after a delay
    /// </summary>
    /// <param name="delaySecs">Time to wait before moving the pinball</param>
    /// <returns>Time to wait before returning to this method</returns>
    private IEnumerator MoveSleepingBodyAfterDelay(float delaySecs)
    {
        yield return new WaitForSeconds(delaySecs);

        // if pinball is still stationary
        if (body.IsSleeping() && onActiveGround && moveSleepingBodyAfterDelayCoroutine != null)
        {
            // sweep area of pinball to determine if movement is possible
            bool canMove = false;
            for (float sweepTestAngle = 0; sweepTestAngle < 360; sweepTestAngle += SWEEP_ANGLE_INCREMENTS)
            {
                // check if movement is possible in the direction of the sweepTestAngle
                Vector3 direction = Quaternion.Euler(0, sweepTestAngle, 0) * activeGroundTriggerCollider.transform.forward;
                if (!body.SweepTest(direction, out RaycastHit hitInfo, col.bounds.extents.x / 4, QueryTriggerInteraction.Ignore))
                {
                    canMove = true;
                    break;
                }
            }

            if (canMove)
            {
                // move the ball by bouncing it upwards
                body.AddForce(activeGroundTriggerCollider.transform.up * SleepBumpForce);
                moveSleepingBodyAfterDelayCoroutine = null;
            }
            else //stuck
            {
                Respawn();
            }
        }
    }


    /// <summary>
    /// Detects if the pinball is on the playfield
    /// </summary>
    /// <param name="other">The collider that was entered</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other == activeGroundTriggerCollider)
        {
            onActiveGround = true;
        }
    }

    /// <summary>
    /// Detects if the pinball is off the playfield
    /// </summary>
    /// <param name="other">The collider that was exited</param>
    private void OnTriggerExit(Collider other)
    {
        if (other == activeGroundTriggerCollider)
        {
            onActiveGround = false;
        }
    }
}
