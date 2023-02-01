/**
 * FILE             : PaddleController.cs
 * PROJECT          : SENG3060-A1
 * PROGRAMMER       : Joshua Murphy
 * FIRST VERSION    : February 1, 2023
 * DESCRIPTION      : Contains the PaddleController class.
 */
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Emulates a paddle in a pinball game.
/// </summary>
public class PaddleController : MonoBehaviour
{
    /// <summary>
    /// Invoked when the paddle is flipped up after being lowered
    /// </summary>
    public UnityEvent Raising;

    /// <summary>
    /// Invoked when the paddle is first released after being raised
    /// </summary>
    public UnityEvent Lowered;

    /// <summary>
    /// Forced to be applied when player flips the paddle forward
    /// </summary>
    public float Force = 3500;

    /// <summary>
    /// Spring force to snap back paddle when the player releases it 
    /// </summary>
    public float Spring = 15;

    // rotation limits of the paddle
    public float MinRotationAngle = -50f;
    public float MaxRotationAngle = 0f;

    [SerializeField]
    private Rigidbody body;
    [SerializeField]
    private HingeJoint joint;

    private bool isRaised = false;

    /// <summary>
    /// Set limits and spring values for the paddle's hinge joint
    /// </summary>
    private void OnValidate()
    {
        if (joint != null)
        {
            // must make copies of joint's fields in order to modify them
            var tempSpring = joint.spring;
            tempSpring.spring = Spring;
            joint.spring = tempSpring;

            var tempLimits = joint.limits;
            tempLimits.min = MinRotationAngle;
            tempLimits.max = MaxRotationAngle;
            joint.limits = tempLimits;
        }
    }

    /// <summary>
    /// Set limits and spring values for the paddle's hinge joint
    /// </summary>
    void Start() 
    {
        // must make copies of joint's fields in order to modify them
        var temp = joint.spring;
        temp.spring = Spring;
        joint.spring = temp;

        var tempLimits = joint.limits;
        tempLimits.min = MinRotationAngle;
        tempLimits.max = MaxRotationAngle;
        joint.limits = tempLimits;
    }

    /// <summary>
    /// Applies a constant forward force to the paddle while it is in the raised state
    /// </summary>
    private void FixedUpdate()
    {
        if (isRaised)
        {
            body.AddForce(body.transform.forward * Force * Time.fixedDeltaTime);
        }
    }

    /// <summary>
    /// Sets whether or not the paddle is raised (pushed forward)
    /// </summary>
    /// <param name="raised">Whether or not the paddle should be raised</param>
    public void SetRaised(bool raised)
    {
        isRaised = raised;
        if (isRaised)
        {
            Raising.Invoke();
        }
        else
        {
            Lowered.Invoke();
        }
    }
}
