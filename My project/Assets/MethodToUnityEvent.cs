using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MethodToUnityEvent : MonoBehaviour
{
    public UnityEvent Event;

    public void DoEvent()
    {
        Event?.Invoke();
    }
}
