using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool isChosen; 
    private float countdownInterval = 1f;
    
    [Header("Selection Phase")]
    public GameObject pnlItems;
    private float pnlTimer = 10;
    private float currentSelectionPhaseTimer;
    private float nextTime;
    private bool isSelectionPhase;
    public TextMeshProUGUI timerText; 
    
    
    [Header("Placement Phase")]
    private bool isPlacementPhase;
    private float placementTimer = 10;
    private float currentPlacementTimer;
    private float nextPlacementTime;
    public TextMeshProUGUI placementTimerText;
    //get playerinputs and should beinactive during this phase and only active once this phase is done 
    
    [Header("Events")] 
    public GameEvent onEndSelectionPhase;
    public GameEvent onEndPlacementPhase;

    void Start()
    {
        // Initialize selection phase
        if (pnlItems != null)
        {
            pnlItems.SetActive(true);
        }
        currentSelectionPhaseTimer = pnlTimer;
        nextTime = Time.time + countdownInterval;
        
        // Initialize placement phase
        currentPlacementTimer = placementTimer;
        nextPlacementTime = Time.time + countdownInterval;
        
        
        //to start selection phase
        isSelectionPhase = true;
    }

    void Update()
    {
        onSelectionTimer();
        if (isPlacementPhase)
        {
            onPlacementTimer();
        }
    }

    private void onSelectionTimer()
    {
        if (currentSelectionPhaseTimer > 0 && Time.time >= nextTime)
        {
            currentSelectionPhaseTimer--;
            nextTime = Time.time + countdownInterval;
        }

        if (currentSelectionPhaseTimer <= 0)
        {
            isSelectionPhase = false;
            currentSelectionPhaseTimer = 0;
            
            //close the item panel that selects an object
            if (pnlItems != null)
            {
                pnlItems.SetActive(false);
                isChosen = true;
            }
            
            //this will end my selection phase
            onEndSelectionPhase.Raise();
            //after selection phase start placement phase
            isPlacementPhase = true;
        }

        timerText.text =
            "Choose your item before the placement phase: " +
            currentSelectionPhaseTimer.ToString(); 
    }
    
    
    // for my placement phase
    private void onPlacementTimer()
    {
        if (currentPlacementTimer > 0 && Time.time >= nextPlacementTime)
        {
            currentPlacementTimer--;
            nextPlacementTime = Time.time + countdownInterval;
        }

        if (currentPlacementTimer <= 0)
        {
            currentPlacementTimer = 0;
            placementTimerText.enabled = false;
            onEndPlacementPhase.Raise();
        }

        placementTimerText.text =
            "Place your item before the time runs out: " +
            currentPlacementTimer.ToString(); // Display the placement timer
    }
    
    //use camera behaviour to zoom out when in pla
}