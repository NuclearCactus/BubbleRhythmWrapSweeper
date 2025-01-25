using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickToUnityEvent : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent Event;


    public void OnPointerClick(PointerEventData eventData)
    {
        Event?.Invoke();
        
    }
}
