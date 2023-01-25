using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddForce(Vector3 force)
    {
        body.AddForce(force);
    }
}
