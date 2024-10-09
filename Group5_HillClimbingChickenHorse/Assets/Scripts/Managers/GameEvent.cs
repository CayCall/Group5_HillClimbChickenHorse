using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent")]
public class GameEvent : ScriptableObject
{
    public List<GameEventListener> listeners = new List<GameEventListener>();

    // Raise event and log the event name
    public void Raise()
    {
        Debug.Log($"Event Raised: {name}");

        // Iterate through listeners and check if any have been destroyed
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            if (listeners[i] == null)
            {
                listeners.RemoveAt(i); // Remove destroyed listener from the list
            }
            else
            {
                listeners[i].OnEventRaised(); // Only call if the listener is valid
            }
        }
    }

    public void RegisterListener(GameEventListener listener)
    {
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if (listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
    }
}