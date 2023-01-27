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

    SpringJoint joint;
    Rigidbody rb;
    bool compress = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        joint = GetComponent<SpringJoint>();
        if (joint != null)
        {
            joint.spring = Spring;
            joint.damper = Damper;
        }
    }

    void OnValidate()
    {
        if (joint != null)
        {
            joint.spring = Spring;
            joint.damper = Damper;
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
        if (compress)
        {
            Compressing.Invoke();
        }
        else
        {
            Released.Invoke();
        }
    }

}
