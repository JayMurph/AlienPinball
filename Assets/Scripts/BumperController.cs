/**
 * FILE             : BumperController.cs
 * PROJECT          : SENG3060-A1
 * PROGRAMMER       : Joshua Murphy
 * FIRST VERSION    : February 1, 2023
 * DESCRIPTION      : Contains the BumperController class
 */
using UnityEngine;

/// <summary>
/// Emulates a pinball bumper. Applies a reflective impulse force to a pinball, upon
/// collision, at a random angle around the Y axis
/// </summary>
public class BumperController : MonoBehaviour
{
    public float Force = 0.32f;

    /// <summary>
    /// Range for random angle at which to relect the pinball around the Y axis
    /// </summary>
    public float ReflectAngleRange = 0f;

    /// <summary>
    /// Identifies pinball object during collision
    /// </summary>
    [SerializeField]
    private string ballTag;

    /// <summary>
    /// Used to avoid recalculating whenever we need to generate a random reflect
    /// angle with 0 as the midpoint
    /// </summary>
    private float reflectAngleHalf = 0;

    /// <summary>
    /// Calculate and store half the provided reflect angle range
    /// </summary>
    void Start()
    {
        if (ReflectAngleRange != 0)
        {
            reflectAngleHalf = ReflectAngleRange / 2;
        }
    }

    /// <summary>
    /// Calculate and store half the provided reflect angle range
    /// </summary>
    private void OnValidate()
    {
        if (ReflectAngleRange != 0)
        {
            reflectAngleHalf = ReflectAngleRange / 2;
        }
    }

    /// <summary>
    /// Detects if colliding object is pinball then bounces it back
    /// </summary>
    /// <param name="collision">information about the collision</param>
    private void OnCollisionEnter(Collision collision)
    {
        OnColliderEnter(collision.collider);
    }

    /// <summary>
    /// Detects if the entering collider is a pinball, then bounces it back in
    /// the opposite direction with an impulse force
    /// </summary>
    /// <param name="other">The entering collider</param>
    private void OnColliderEnter(Collider other)
    {
        // if we collided with the pinball
        if (other.gameObject.CompareTag(ballTag))
        {
            BallController ball = other.gameObject.GetComponent<BallController>();

            if (ball != null)
            {
                Vector3 reflectionDirection = (transform.position - other.ClosestPointOnBounds(transform.position)).normalized * -1;

                if (reflectAngleHalf != 0)
                {
                    // randomize the angle of the ball reflection
                    float randomAngle = Random.Range(-reflectAngleHalf, reflectAngleHalf);
                    reflectionDirection = Quaternion.Euler(new Vector3(0, randomAngle, 0)) * reflectionDirection;
                }

                ball.AddImpulse(reflectionDirection * Force);
            }
        }
    }
}
