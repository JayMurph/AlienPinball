using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShotController : MonoBehaviour
{
    public float Force = 1500;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        BallController ball = collision.gameObject.GetComponent<BallController>();

        if (ball != null)
        {
            ball.AddForce(collision.GetContact(0).normal * -1 * Force);
        }
    }
}
