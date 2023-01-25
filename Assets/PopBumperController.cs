using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopBumperController : MonoBehaviour
{
    public float Force = 100f;

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
        Debug.Log("1");
        BallController ball = collision.gameObject.GetComponent<BallController>();

        if (ball != null)
        {
            Debug.Log("2");
            ball.AddForce(collision.transform.forward * -1 * Force);
        }
    }
}
