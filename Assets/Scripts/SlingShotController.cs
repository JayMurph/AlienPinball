/**
 * FILE             : SlingShotController.cs
 * PROJECT          : SENG3060-A1
 * PROGRAMMER       : Joshua Murphy
 * FIRST VERSION    : February 1, 2023
 * DESCRIPTION      : Contains the SlingShotController class
 */
using UnityEngine;
using UnityEngine.ProBuilder;

/// <summary>
/// Emulates a pinball slingshot bumper that applies a reflective force to
/// to a pinball upon collision
/// </summary>
public class SlingShotController : MonoBehaviour
{
    public float Force;

    /// <summary>
    /// Tag for identifying pinball game object
    /// </summary>
    [SerializeField]
    private string pinballTag;

    private void OnCollisionEnter(Collision collision)
    {
        OnColliderEnter(collision.collider);
    }

    /// <summary>
    /// Determines if the entering collider is a pinball, then applies a force
    /// to the ball in the oppopsite direction of the collision
    /// </summary>
    /// <param name="collider">The collider that is staying</param>
    private void OnColliderEnter(Collider collider)
    {
        // Check if we were hit by the pinball
        if (collider.gameObject.tag == pinballTag)
        {
            BallController ball = collider.GetComponent<BallController>();

            if (ball != null)
            {
                // get the normal of the collision and make it parallel it to the floor -
                // this is done to avoid weird collisions with slingshot's mesh
                Vector3 collisionDirection = (transform.position - collider.ClosestPointOnBounds(transform.position)).normalized;
                Quaternion collisionRotation = Quaternion.LookRotation(collisionDirection);
                Vector3 floorAlignedCollisionNormal = 
                    Quaternion.Euler(collisionRotation.eulerAngles.x, 0, collisionRotation.eulerAngles.z) * 
                    Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z) *    
                    collisionDirection;

                // reverse the floor aligned collision normal
                Vector3 reflectionDirection = floorAlignedCollisionNormal * -1;

                // force the ball back in the opposite direction of the collision
                ball.AddForce(reflectionDirection * Force);
            }
        }
    }
}
