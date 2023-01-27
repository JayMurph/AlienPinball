using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallDeathPlaneController : MonoBehaviour
{
    public UnityEvent BallCollision;

    [SerializeField]
    private string ballTag;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ballTag)
        {
            Destroy(collision.gameObject);
            BallCollision.Invoke();
        }
    }
}
