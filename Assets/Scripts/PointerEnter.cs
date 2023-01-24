using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PointerEnter : MonoBehaviour, IPointerEnterHandler
{
    public UnityEvent OnPointerEnterEvent;
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerEnterEvent.Invoke();
    }
}
