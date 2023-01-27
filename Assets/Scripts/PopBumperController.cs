using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopBumperController : MonoBehaviour
{
    public float Force = 5000f;
    public float ReflectAngleRange = 60f;

    [SerializeField]
    private string ballTag;

    private float reflectAngleHalf;

    // Start is called before the first frame update
    void Start()
    {
        reflectAngleHalf = ReflectAngleRange / 2;
    }

    private void OnValidate()
    {
        reflectAngleHalf = ReflectAngleRange / 2;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ballTag)
        {
            BallController ball = collision.gameObject.GetComponent<BallController>();

            if (ball != null)
            {
                float angle = Random.Range(-reflectAngleHalf, reflectAngleHalf);
                Vector3 dir = Quaternion.Euler(new Vector3(0, angle, 0)) * (collision.GetContact(0).normal * -1);
                ball.AddForce(dir * Force);
            }
        }
    }
}
