using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class BallController : MonoBehaviour
{
    public float MaxSleepTimeSecs;
    public float SleepBumpForce;

    [SerializeField]
    private Collider activeGroundTriggerCollider;
    [SerializeField]
    private Transform respawnPoint;

    Rigidbody body;
    Collider col;

    private bool onActiveGround = false;
    private Coroutine moveSleepingBodyAfterDelayCoroutine;
    private readonly float sweepAngleIncrements = 30f;
    public void Respawn()
    {
        transform.position = respawnPoint.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();        
        col = GetComponent<Collider>();
    }

    public void AddForce(Vector3 force)
    {
        body.AddForce(force);
    }

    private void Update()
    {
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

    private IEnumerator MoveSleepingBodyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (body.IsSleeping() && onActiveGround && moveSleepingBodyAfterDelayCoroutine != null)
        {
            Vector3 moveDir = Vector3.zero;
            bool canMove = false;
            for (float i = 0; i < 360; i += sweepAngleIncrements)
            {
                moveDir = Quaternion.Euler(0, sweepAngleIncrements, 0) * activeGroundTriggerCollider.transform.forward;
                if (!body.SweepTest(moveDir, out RaycastHit hitInfo, col.bounds.extents.x / 4, QueryTriggerInteraction.Ignore))
                {
                    canMove = true;
                    break;
                }
            }

            if (canMove)
            {
                body.AddForce(activeGroundTriggerCollider.transform.up);
                moveSleepingBodyAfterDelayCoroutine = null;
            }
            else //stuck
            {
                Respawn();
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other == activeGroundTriggerCollider)
        {
            onActiveGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == activeGroundTriggerCollider)
        {
            onActiveGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == activeGroundTriggerCollider)
        {
            onActiveGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider == activeGroundTriggerCollider)
        {
            onActiveGround = false;
        }
    }
}
