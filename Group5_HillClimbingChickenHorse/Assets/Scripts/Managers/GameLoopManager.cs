using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoopManager : MonoBehaviour
{
    [SerializeField] private int numberOfRounds;
    [SerializeField] private int currentRound;
    [SerializeField] private string nextLevel;
    [SerializeField] private Transform startPosition1;
    [SerializeField] private Transform startPosition2;
    [SerializeField] private GameObject wheel1;
    [SerializeField] private GameObject wheel2;
    [SerializeField] private GameManager gameManager;

    private void Start()
    {
        currentRound = 0;
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }
    
    private void CheckNextStep()
    {
   
        if (currentRound < numberOfRounds )
        {
            wheel1.transform.position = startPosition1.position;
            wheel2.transform.position = startPosition2.position;
            currentRound++;
            gameManager.StartSelectionPhase();
        }
        else
        {
            SceneManager.LoadScene(nextLevel);
        }
    }

    private void ResetPlayer()
    {
        wheel1.transform.position = startPosition1.position;
        wheel2.transform.position = startPosition2.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            Debug.Log("Hit");
            ResetPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("End"))
        {
            CheckNextStep();
        }
        else
        {
            Debug.LogError("Get chowed");
        }
    }
}
