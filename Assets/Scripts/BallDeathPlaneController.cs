using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallDeathPlaneController : MonoBehaviour
{
    public UnityEvent BallCollision;

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
        if (collision.gameObject.tag == "Ball")
        {
            BallController ball = collision.gameObject.GetComponent<BallController>();
            if (ball != null)
            {
                Debug.Log("BallCollision");
                Destroy(ball.gameObject);
                BallCollision.Invoke();
            }
        }
    }
}
