using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SpringJoint))]
public class PlungerController : MonoBehaviour
{
    public float CompressForce = 400f;
    public float Spring = 100f;

    SpringJoint joint;
    Rigidbody rb;
    bool compress = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        joint = GetComponent<SpringJoint>();
    }

    private void OnValidate()
    {
        if (joint != null)
        {
            joint.spring = Spring;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if(compress)
        {
            joint.spring = 0;
            rb.AddForce(transform.forward * -1 * CompressForce * Time.fixedDeltaTime);
        }
        else if (joint.spring == 0)
        {
            joint.spring = Spring;
        }
    }

    public void Compress(bool c)
    {
        compress = c;
    }

}
