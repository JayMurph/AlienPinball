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
