/**
 * FILE             : SlingShotController.cs
 * PROJECT          : SENG3060-A1
 * PROGRAMMER       : Joshua Murphy
 * FIRST VERSION    : February 1, 2023
 * DESCRIPTION      : Contains the SlingShotController class
 */
using UnityEngine;

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

    /// <summary>
    /// Determines if the colliding object is a pinball, then applies a force
    /// to the ball in the oppopsite direction of the collision
    /// </summary>
    /// <param name="collision">Information about the collision</param>
    void OnCollisionEnter(Collision collision)
    {
        // Check if were hit by the pinball
        if (collision.gameObject.tag == pinballTag)
        {
            BallController ball = collision.gameObject.GetComponent<BallController>();

            if (ball != null)
            {
                Vector3 collisionNormal = collision.GetContact(0).normal;

                /*
                 * HACK. because slingshot model is jagged, there is a
                 * possibility to bounce the ball up, so well use our
                 * transforms tilt as the y component for reflection. NEGATE it
                 * because slingshot prefab is upside down
                 */
                collisionNormal.y = -transform.forward.y; 

                // force the ball back in the opposite direction of the collision
                ball.AddForce(collisionNormal * -1 * Force);
            }
        }
    }
}
