/**
 * FILE             : PlungerController.cs
 * PROJECT          : SENG3060-A1
 * PROGRAMMER       : Joshua Murphy
 * FIRST VERSION    : February 1, 2023
 * DESCRIPTION      : Contains the PlungerController class
 */
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Emulates a pinball plunger. During compression, a constant force is applied
/// to the plunger to push it back. When released, the spring value is applied
/// to the plunger's SpringJoint so that it snaps back
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SpringJoint))]
public class PlungerController : MonoBehaviour
{
    /// <summary>
    /// Invoked when the plunger first begins being compressed
    /// </summary>
    public UnityEvent Compressing;

    /// <summary>
    /// Invoked when the plunger is released after being compressed
    /// </summary>
    public UnityEvent Released;

    public float CompressionForce = 35;
    public float Spring = 400;
    public float Damper = 4;

    /// <summary>
    /// Gate to open when pinball is interacting with plunger, so that the
    /// pinball can enter the play area
    /// </summary>
    [SerializeField]
    private GateController gate;

    /// <summary>
    /// Identifies pinball during collisions
    /// </summary>
    [SerializeField]
    private string ballTag;

    private SpringJoint joint;
    private Rigidbody body;

    private bool isCompressed = false;

    /// <summary>
    /// Acquire rigidbody and springjoing, then initialize springjoint
    /// </summary>
    void Start()
    {
        body = GetComponent<Rigidbody>();
        joint = GetComponent<SpringJoint>();

        joint.spring = Spring;
        joint.damper = Damper;
    }

    /// <summary>
    /// Acquire rigidbody and springjoing, then initialize springjoint
    /// </summary>
    void OnValidate()
    {
        body = GetComponent<Rigidbody>();
        joint = GetComponent<SpringJoint>();

        joint.spring = Spring;
        joint.damper = Damper;
    }

    /// <summary>
    /// Either applies force to the plunger or activates its spring, depending
    /// on if it is being compressed
    /// </summary>
    private void FixedUpdate()
    {
        if(isCompressed)
        {
            joint.spring = 0; // without this, the spring puts a hard limit on how far back the plunger can go
            //push the plunger backwards
            body.AddForce(transform.forward * -1 * CompressionForce * Time.fixedDeltaTime);
        }
        else if (joint.spring == 0)
        {
            joint.spring = Spring; //engage spring
        }
    }

    /// <summary>
    /// Changes whether the plunger is being compressed or not
    /// </summary>
    /// <param name="compress">Whether the plugner should begin compressing or be released</param>
    public void Compress(bool compress)
    {
        isCompressed = compress;
        if (isCompressed)
        {
            Compressing.Invoke();
        }
        else
        {
            Released.Invoke();
        }
    }

    /// <summary>
    /// If the plunger is colliding with the pinball, opens a gate so that the
    /// pinball can enter the play area after being shot from the plunger
    /// </summary>
    /// <param name="collision">Information about the collision</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ballTag)
        {
            gate.Open();
        }
    }
}
