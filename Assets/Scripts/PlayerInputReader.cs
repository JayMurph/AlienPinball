/**
 * FILE             : PlayerInputReader.cs
 * PROJECT          : SENG3060-A1
 * PROGRAMMER       : Joshua Murphy
 * FIRST VERSION    : February 1, 2023
 * DESCRIPTION      : Contains the PlayerInputReader class.
 */
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// Translates from input messages to concrete actions of the player in the
/// pinball game. 
/// </summary>
public class PlayerInputReader : MonoBehaviour
{
    /// <summary>
    /// Invoked when the player begins compressing or releases the
    /// plunger. True value indicates compression.
    /// </summary>
    public UnityEvent<bool> OnPlungerEvent;

    /// <summary>
    /// Invoked when the player flips or releases the left paddle. True value
    /// indicates that the player is flipping the paddle up.
    /// </summary>
    public UnityEvent<bool> OnPaddleLeftEvent;

    /// <summary>
    /// Invoked when the player flips or releases the right paddle. True value
    /// indicates that the player is flipping the paddle up.
    /// </summary>
    public UnityEvent<bool> OnPaddleRightEvent;

    /// <summary>
    /// To be invoked by input action message
    /// </summary>
    /// <param name="val">Contains float input value</param>
    public void OnPlunger(InputValue val)
    {
        OnPlungerEvent.Invoke(val.Get<float>() > 0);
    }

    /// <summary>
    /// To be invoked by input action message
    /// </summary>
    /// <param name="val">Contains float input value</param>
    public void OnPaddleLeft(InputValue val)
    {
        OnPaddleLeftEvent.Invoke(val.Get<float>() > 0);
    }

    /// <summary>
    /// To be invoked by input action message
    /// </summary>
    /// <param name="val">Contains float input value</param>
    public void OnPaddleRight(InputValue val)
    {
        OnPaddleRightEvent.Invoke(val.Get<float>() > 0);
    }
}
