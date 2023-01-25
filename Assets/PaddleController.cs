using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float Force = 100f;

    public Rigidbody paddleBody;
    bool isUp = false;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    private void FixedUpdate()
    {
        if (isUp)
        {
            paddleBody.AddForce(paddleBody.transform.forward * Force * Time.fixedDeltaTime);
        }
    }


    public void SetPosition(bool up)
    {
        isUp = up;
    }
}
