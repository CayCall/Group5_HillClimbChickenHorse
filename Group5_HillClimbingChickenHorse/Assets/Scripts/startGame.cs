using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class startGame : MonoBehaviour
{
    [SerializeField]private SceneLoaders _sceneLoaders;

    private void Update()
    {
        StartGame();
    }

    private void StartGame()
    {
        if (Gamepad.current != null && Gamepad.current.enabled)
        {
            var select = Gamepad.current.buttonSouth.isPressed;

            if (select)
            {
                _sceneLoaders.LoadMainScene();
            }
        }
    }
}
