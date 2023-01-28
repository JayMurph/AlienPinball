using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class BallController : MonoBehaviour
{
    public float MaxSleepTimeSecs;
    public float SleepBumpForce;

    [SerializeField]
    private Collider activeGroundTriggerCollider;

    Rigidbody body;
    Collider collider;

    private bool onActiveGround = false;
    private Coroutine moveSleepingBodyAfterDelayCoroutine;
    private readonly float sweepAngleIncrements = 30f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();        
        collider = GetComponent<Collider>();
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
            for (float i = 0; i < 360; i += sweepAngleIncrements)
            {
                moveDir = Quaternion.Euler(0, sweepAngleIncrements, 0) * transform.forward;
                if (!body.SweepTest(moveDir, out RaycastHit hitInfo, collider.bounds.extents.x * 1, QueryTriggerInteraction.Ignore))
                {
                    break;
                }
            }
            body.AddForce(moveDir);
            moveSleepingBodyAfterDelayCoroutine = null;
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
