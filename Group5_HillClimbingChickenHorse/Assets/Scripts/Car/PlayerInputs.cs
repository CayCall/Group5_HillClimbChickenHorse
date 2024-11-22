using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using CC;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInputs : MonoBehaviour
{
    public Vector2 cursorMoveDirction;
    public float throttle;
    public bool useKeyboard;
    public float positive;
    public float negative;

    //pause menu
    public GameObject pauseMenu;
    private bool isOpened = false;
    [SerializeField] private AudioSource pauseSound;
    public GameObject wheel;
    public GameObject wheelPos;
    
    private void Update()
    {
        // Continuously call input methods
        Gas();
        Cursor();
        Select();
        PauseGame();
    }

    // Method to detect gas (acceleration) inputs from both Gamepad and Keyboard
    public void Gas()
    {
        positive = 0f;
        negative = 0f;

        if (Gamepad.current != null && Gamepad.current.enabled)  // Ensure gamepad is connected
        {
            positive = Gamepad.current.rightTrigger.ReadValue(); 
            negative = Gamepad.current.leftTrigger.ReadValue();
       
        }
        if (Keyboard.current != null && useKeyboard)  // Fallback to keyboard input
        {
            positive = Keyboard.current.rightArrowKey.ReadValue();
            negative = Keyboard.current.leftArrowKey.ReadValue();
            Debug.Log("test");
        }

        throttle = positive - negative;
        //Debug.Log("Direction is " + throttle);
    }
    

    // Method to continuously track cursor movement (e.g., left stick on gamepad)
    public void Cursor()
    {
        if (Gamepad.current != null && Gamepad.current.enabled)
        {
            cursorMoveDirction = Gamepad.current.leftStick.ReadValue();
           // Debug.Log("Cursor moves in the direction " + cursorMoveDirction);
        }
        // You can add keyboard movement or mouse input logic here if needed.
    }

    // Method to continuously detect the select button press (A button on Xbox)
    public void Select()
    {
        if (Gamepad.current != null && Gamepad.current.enabled)
        {
            var select = Gamepad.current.buttonSouth.isPressed;

            if (select)
            {
                //Debug.Log("Select button was pressed");
            }
        }
    }
  
    public bool NavigateDown()
    {
        // Down D-Pad 
        return Gamepad.current.dpad.down.wasPressedThisFrame;
    }

    public bool SelectItem()
    {
        // Button South press (e.g., A/X)
        return Gamepad.current.buttonSouth.wasPressedThisFrame;
    }

    public bool ConfirmPlacement()
    {
        // Check if Button South is pressed again for placement
        return Gamepad.current.buttonSouth.wasPressedThisFrame;
    }


    public void PauseGame()
    {
        if (Gamepad.current != null && Gamepad.current.enabled)
        {
            // Detect button press (not hold)
            if (Gamepad.current.startButton.wasPressedThisFrame && pauseMenu != null)
            {
                isOpened = !isOpened; 
                pauseMenu.SetActive(isOpened); 
                Time.timeScale = isOpened ? 0f : 1f; 
                pauseSound.Play();
                // Play sound for toggling the pause menu
                if (pauseSound!= null)
                {
                    pauseSound.Play();
                }
            }
        }

        if (isOpened)
        {
            if (Gamepad.current.buttonNorth.wasPressedThisFrame)
            {
                Debug.Log("ButtonNorth was pressed");
                wheel.transform.localPosition = wheel.transform.parent.InverseTransformPoint(wheelPos.transform.position);              
                isOpened = false; 
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
            }
            if (Gamepad.current.buttonEast.wasPressedThisFrame)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                isOpened = false; 
                pauseMenu.SetActive(false); 
                Time.timeScale = 1f;
            }
        }
    }
}