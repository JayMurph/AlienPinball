using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SpringJoint))]
public class PlungerController : MonoBehaviour
{
    public UnityEvent Compressing;
    public UnityEvent Released;

    public float CompressForce = 400f;
    public float Spring = 100f;
    public float Damper = 4f;

    private SpringJoint joint;
    private Rigidbody body;
    private bool isCompressed = false;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        joint = GetComponent<SpringJoint>();

        joint.spring = Spring;
        joint.damper = Damper;
    }

    void OnValidate()
    {
        body = GetComponent<Rigidbody>();
        joint = GetComponent<SpringJoint>();

        joint.spring = Spring;
        joint.damper = Damper;
    }

    private void FixedUpdate()
    {
        if(isCompressed)
        {
            joint.spring = 0;
            body.AddForce(transform.forward * -1 * CompressForce * Time.fixedDeltaTime);
        }
        else if (joint.spring == 0)
        {
            joint.spring = Spring;
        }
    }

    public void Compress(bool c)
    {
        isCompressed = c;
        if (isCompressed)
        {
            Compressing.Invoke();
        }
        else
        {
            Released.Invoke();
        }
    }

}
