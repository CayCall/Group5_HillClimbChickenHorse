using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{


    [SerializeField] private bool isChosen;

// temp for milestone 1
    [Header("Temp")] 
    public GameObject cursor; 
    public GameObject pnlItems;
    private float pnlTimer = 5;
    private float currentpnlTimer;
    private float countdownInterval = 1f; 
    private float nextTime;
    public TextMeshProUGUI timerText; 
    
    
    
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
        // 'B' button or "Fire2" input for placing 
        if (isChosen && Input.GetButtonDown("PlaceButton"))
        {
            Vector2 cursorPosition = FindObjectOfType<Cursor_script>().cursorObject.position;
            FindObjectOfType<Cursor_script>().InstantiateSelectedObject(cursorPosition);
            isChosen = false;
        }
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

        if (currentpnlTimer == 0)
        {
            cursor.SetActive(false);   
        }
        
        UpdateTimerDisplay();
        Debug.Log(currentpnlTimer);
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
        }
    }
}