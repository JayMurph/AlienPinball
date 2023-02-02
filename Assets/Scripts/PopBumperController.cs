/**
 * FILE             : PopBumperController.cs
 * PROJECT          : SENG3060-A1
 * PROGRAMMER       : Joshua Murphy
 * FIRST VERSION    : February 1, 2023
 * DESCRIPTION      : Contains the PopBumperController class
 */
using UnityEngine;

/// <summary>
/// Emulates a pinball pop bumper. Applies a reflective force to a pinball,
/// upon collision, at a random angle around the Y axis
/// </summary>
public class PopBumperController : MonoBehaviour
{
    public float Force = 14f;

    /// <summary>
    /// Range for random angle at which to relect the pinball around the Y axis
    /// </summary>
    public float ReflectAngleRange = 60f;

    /// <summary>
    /// Identifies pinball object during collision
    /// </summary>
    [SerializeField]
    private string ballTag;

    /// <summary>
    /// Used to avoid recalculating whenever we need to generate a random reflect
    /// angle with 0 as the midpoint
    /// </summary>
    private float reflectAngleHalf;

    /// <summary>
    /// Calculate and store half the provided reflect angle range
    /// </summary>
    void Start()
    {
        reflectAngleHalf = ReflectAngleRange / 2;
    }

    /// <summary>
    /// Calculate and store half the provided reflect angle range
    /// </summary>
    private void OnValidate()
    {
        reflectAngleHalf = ReflectAngleRange / 2;
    }

    /// <summary>
    /// Determines if a pinball collided with the object in which case it
    /// bounces the ball back at a random angle around the Y axis within the
    /// given range
    /// </summary>
    /// <param name="collision">Information about the collision</param>
    void OnCollisionEnter(Collision collision)
    {
        // if we collided with the pinball
        if (collision.gameObject.CompareTag(ballTag))
        {
            BallController ball = collision.gameObject.GetComponent<BallController>();

            if (ball != null)
            {
                // force the pinball back at a random angle around the collision y axis
                float randomAngle = Random.Range(-reflectAngleHalf, reflectAngleHalf);
                Vector3 reflectDirection = Quaternion.Euler(new Vector3(0, randomAngle, 0)) * (collision.GetContact(0).normal * -1);
                ball.AddForce(reflectDirection * Force);
            }
        }
    }
}
