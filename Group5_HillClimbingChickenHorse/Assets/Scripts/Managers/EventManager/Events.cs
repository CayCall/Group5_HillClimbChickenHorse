using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Events : MonoBehaviour
{
    public static Events EventsInstance;

    // Start is called before the first frame update
    private void Awake()
    {
        EventsInstance = this;
    }

    public event Action onSelectionPhase;

    public void startSelectionPhase()
    {
        
        // use after start button is pressed 
        if (onSelectionPhase != null)
        {
            onSelectionPhase();
        }
    }
    
    
    // use when placement phase starts
    public event Action onPlacementPhase;
    
}

