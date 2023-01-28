using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BallSpeedZone : MonoBehaviour
{
    public Vector3 Direction = Vector3.forward;
    public float Force;
    public float StationaryDelaySecs;

    [SerializeField]
    private string ballTag;

    private BallController ball;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Direction * Force); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ballTag)
        {
            ball = other.gameObject.GetComponent<BallController>();
            StartCoroutine(ApplyForceAfterDelay(StationaryDelaySecs));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == ballTag)
        {
            ball = null;
        }
    }

    private IEnumerator ApplyForceAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (ball != null)
        {
            ball.AddForce(Direction * Force); 
        } 
    }
}
