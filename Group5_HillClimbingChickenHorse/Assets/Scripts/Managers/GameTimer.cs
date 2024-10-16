using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameTimer : MonoBehaviour
{
    public float currentTime;
    private float countdownInterval = 1;
    [SerializeField]private float Timer = 60;
    private TextMeshProUGUI timerText;

    private void Start()
    {
        timerText = GameObject.Find("Timer_Text").GetComponent<TextMeshProUGUI>();
        currentTime = Timer;
    }

    public void startTimer()
    {
        currentTime -= Time.deltaTime;
        if(currentTime >= 0 )
        {

            timerText.text = "Time remaining:" + currentTime.ToString("F2");
            
        }

    }
}
