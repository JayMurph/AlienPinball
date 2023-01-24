using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float Force = 100f;

    HingeJoint joint;
    Rigidbody rb;

    bool isUp = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        joint = GetComponent<HingeJoint>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (isUp)
        {
            rb.AddForce(transform.forward * Force * Time.fixedDeltaTime);
        }
    }


    public void SetPosition(bool up)
    {
        isUp = up;
    }
}
