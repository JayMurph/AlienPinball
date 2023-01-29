using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonBehaviour : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public UnityEvent Enter;
    public UnityEvent Click;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Enter.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Click.Invoke();
    }
}
