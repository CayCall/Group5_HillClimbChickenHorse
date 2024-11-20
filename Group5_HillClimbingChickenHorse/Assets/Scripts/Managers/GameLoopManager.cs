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
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private GameObject player;
    [SerializeField] private GameManager gameManager;

    private void Start()
    {
        currentRound = 0;
        player = gameObject;
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("End"))
        {
            CheckNextStep();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Obstacle"))
        {
            ResetPlayer();
        }
    }

    private void CheckNextStep()
    {
        if (currentRound != numberOfRounds)
        {
            player.transform.position = startPosition;
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
        player.transform.position = startPosition;
    }
}
