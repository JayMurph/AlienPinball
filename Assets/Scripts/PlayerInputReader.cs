using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{
    public UnityEvent<bool> OnPlungerEvent;
    public UnityEvent<bool> OnPaddleLeftEvent;
    public UnityEvent<bool> OnPaddleRightEvent;

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
        OnPlungerEvent.Invoke(val.Get<float>() > 0);
    }

    public void OnPaddleLeft(InputValue val)
    {
        OnPaddleLeftEvent.Invoke(val.Get<float>() > 0);
    }
    public void OnPaddleRight(InputValue val)
    {
        OnPaddleRightEvent.Invoke(val.Get<float>() > 0);
    }
}
