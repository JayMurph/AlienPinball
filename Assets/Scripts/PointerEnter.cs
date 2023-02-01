/**
 * FILE             : PointerEnter.cs
 * PROJECT          : SENG3060-A1
 * PROGRAMMER       : Joshua Murphy
 * FIRST VERSION    : February 1, 2023
 * DESCRIPTION      : Contains the PointerEnter class
 */
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

/// <summary>
/// Invokes a UnityEvent when a point enters an object
/// </summary>
public class PointerEnter : MonoBehaviour, IPointerEnterHandler
{
    public UnityEvent OnPointerEnterEvent;

    /// <summary>
    /// Implements IPointerEnterHandler interface. Invokes the PointerEnter
    /// object's UnityEvent
    /// </summary>
    /// <param name="eventData">Unused</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerEnterEvent.Invoke();
    }
}
