using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool isChosen;
    
    public GameObject pnlItems;
    private float pnlTimer = 5;
    private float currentpnlTimer;
    private float countdownInterval = 1f; 
    private float nextTime;
    public TextMeshProUGUI timerText;

    [Header("Events")] public GameEvent onEndPlacementPhase;
    
    
    
    void Start()
    {
        if (pnlItems != null)
        {
            pnlItems.SetActive(true);
        }

        currentpnlTimer = pnlTimer;
        nextTime = Time.time + countdownInterval;
        
    }
    
    void Update()
    {
        if ( currentpnlTimer > 0 && Time.time >= nextTime)
        {
            currentpnlTimer--; 
            nextTime = Time.time + countdownInterval; 
          
        }

        if ( currentpnlTimer <= 0)
        {
            currentpnlTimer = 0;
            ClosePanel();
        }


        UpdateTimerDisplay();
    }
    void UpdateTimerDisplay()
    {
        timerText.text = "Choose your item before the placement phase: " + currentpnlTimer.ToString(); // Display the timer
    }

    public void ClosePanel()
    {
        if (pnlItems != null)
        {
            pnlItems.SetActive(false);
            isChosen = true;
            onEndPlacementPhase.Raise();
            Debug.Log("Closed Panel");
        }
    }
}