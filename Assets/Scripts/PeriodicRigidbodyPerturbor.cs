/**
 * FILE             : PeriodicRigidbodyPerturbor.cs
 * PROJECT          : SENG3060-A1
 * PROGRAMMER       : Joshua Murphy
 * FIRST VERSION    : February 1, 2023
 * DESCRIPTION      : Contains the PeriodictRigidbodyPerturbor class
 */
using System.Collections;
using UnityEngine;

/// <summary>
/// Applies a force to a rigidbody at random intervals and at random angles
/// around the provided axis
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PeriodicRigidbodyPerturbor : MonoBehaviour
{
    /// <summary>
    /// Minimum amount of time to periodically apply force
    /// </summary>
    public float MinSecs;

    /// <summary>
    /// Maximum amount of time to periodically apply force
    /// </summary>
    public float MaxSecs;

    public float Force;

    /// <summary>
    /// axis along which to apply force at a random angle
    /// </summary>
    public Vector3 Axis; 

    /// <summary>
    /// will have force applied to it at random intervals and angles
    /// </summary>
    private Rigidbody body; 

    /// <summary>
    /// Starts a coroutine to periodically apply force to the associated
    /// Rigidbody
    /// </summary>
    void Start()
    {
        body = GetComponent<Rigidbody>();
        StartCoroutine(Perturb());
    }

    /// <summary>
    /// Draws the axis of the force that will periodically be applied by the
    /// component
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Axis);
    }

    /// <summary>
    /// Loops endlessly and periodically applies a force to the body field at a
    /// random angle
    /// </summary>
    /// <returns>
    /// </returns>
    private IEnumerator Perturb()
    {
        while (true)
        {
            // wait for some random time
            yield return new WaitForSeconds(Random.Range(MinSecs, MaxSecs));

            // use random angle along Axis as a direction to apply force in
            Vector3 direction = Quaternion.AngleAxis(Random.Range(0, 360), Axis).eulerAngles;
            body.AddForce(direction * Force, ForceMode.Force);
        }
    }
}
