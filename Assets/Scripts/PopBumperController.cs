using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopBumperController : MonoBehaviour
{
    public float Force = 5000f;
    public float ReflectAngleRange = 60f;

    float reflectAngleHalf;

    // Start is called before the first frame update
    void Start()
    {
        reflectAngleHalf = ReflectAngleRange / 2;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnValidate()
    {
        reflectAngleHalf = ReflectAngleRange / 2;
    }

    void OnCollisionEnter(Collision collision)
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
