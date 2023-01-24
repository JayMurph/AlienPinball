using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{
    public PlungerController plunger;
    public PaddleController paddleLeft;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
    }

    public void OnPlunger(InputValue val)
    {
        plunger.Compress(val.Get<float>() > 0);
    }

    public void OnPaddleLeft(InputValue val)
    {
        paddleLeft.SetPosition(val.Get<float>() > 0);
    }
}
