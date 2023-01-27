using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShotController : MonoBehaviour
{
    public float Force;

    [SerializeField]
    private string ballTag;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ballTag)
        {
            BallController ball = collision.gameObject.GetComponent<BallController>();

            if (ball != null)
            {
                ball.AddForce(collision.GetContact(0).normal * -1 * Force);
            }
        }
    }
}
