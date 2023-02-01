/**
 * FILE             : CollisionDetector.cs
 * PROJECT          : SENG3060-A1
 * PROGRAMMER       : Joshua Murphy
 * FIRST VERSION    : February 1, 2023
 * DESCRIPTION      : Contains the CollisionDetector class
 */
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Invokes an event when it collides with an object with a specific tag
/// </summary>
public class CollisionDetector : MonoBehaviour
{
    /// <summary>
    /// Invoked when colliding with an object with the specified tag
    /// </summary>
    public UnityEvent Collision;

    /// <summary>
    /// Tag of object to emit Collision events for
    /// </summary>
    [SerializeField]
    private string collisionObjectTag;

    /// <summary>
    /// Invokes the Collision event if colliding with an object with the
    /// specified tag
    /// </summary>
    /// <param name="collision">Information about the collision</param>
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == collisionObjectTag)
        {
            Collision.Invoke();
        }
    }
}
