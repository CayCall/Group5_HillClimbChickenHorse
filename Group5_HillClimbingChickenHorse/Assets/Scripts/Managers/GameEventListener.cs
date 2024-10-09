using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent GameEvent;
    public UnityEvent response;

    private void OnEnable()
    {
        GameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        GameEvent.UnregisterListener(this);
    }

    // Log when the event is received and invoke the response
    public void OnEventRaised()
    {
        Debug.Log($"{gameObject.name} received event: {GameEvent.name}");  // Log the listener and event name
        response.Invoke();
    }
}                                                                                                                   