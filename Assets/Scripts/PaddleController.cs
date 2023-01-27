using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class PaddleController : MonoBehaviour
{
    public UnityEvent Raising;
    public UnityEvent Lowered;

    public float Force = 3500;
    public float Spring = 15;
    public float Min = -50f;
    public float Max = 0f;

    [SerializeField]
    private Rigidbody body;
    [SerializeField]
    private HingeJoint joint;

    private bool isRaised = false;

    private void OnValidate()
    {
        if (joint != null)
        {
            var tempSpring = joint.spring;
            tempSpring.spring = Spring;
            joint.spring = tempSpring;

            var tempLimits = joint.limits;
            tempLimits.min = Min;
            tempLimits.max = Max;
            joint.limits = tempLimits;
        }
    }

    // Start is called before the first frame update
    void Start() 
    {
        var temp = joint.spring;
        temp.spring = Spring;
        joint.spring = temp;
    }

    private void FixedUpdate()
    {
        if (isRaised)
        {
            body.AddForce(body.transform.forward * Force * Time.fixedDeltaTime);
        }
    }

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
