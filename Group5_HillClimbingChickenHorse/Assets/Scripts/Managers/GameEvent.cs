using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(menuName = "GameEvent")]
public class GameEvent : ScriptableObject
{
    public List<GameEventListener> listeners = new List<GameEventListener>();
    //raise event through different methods
    public void Raise()
    {
        for (int i = 0; i < listeners.Count; i++)
        {
            listeners[i].OnEventRaised();
        }    
    }

    public void RegisterListener(GameEventListener listener)
    {
        if(!listeners.Contains(listener))
            listeners.Add(listener);
    }
    
    public void UnregisterListener(GameEventListener listener)
    {
        if(!listeners.Contains(listener))
            listeners.Remove(listener);
    }

}
