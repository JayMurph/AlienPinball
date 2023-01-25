using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PaddleController : MonoBehaviour
{
    public float Force = 150000f;
    public float Spring = 1000f;
    public float Min = -50f;
    public float Max = 0f;

    public Rigidbody paddleBody;
    public HingeJoint joint;

    private bool isUp = false;

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
        if (joint != null)
        {
            var temp = joint.spring;
            temp.spring = Spring;
            joint.spring = temp;
        }
    }

    // Update is called once per frame
    void Update() 
    {
    }

    private void FixedUpdate()
    {
        if (isUp)
        {
            paddleBody.AddForce(paddleBody.transform.forward * Force * Time.fixedDeltaTime);
        }
    }


    public void SetPosition(bool up)
    {
        isUp = up;
    }
}
