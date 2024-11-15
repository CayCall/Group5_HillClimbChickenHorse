using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using CC;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    public Vector2 cursorMoveDirction;
    public float throttle;
    public bool useKeyboard;
    public float positive;
    public float negative;

    private void Update()
    {
        // Continuously call input methods
        Gas();
        Cursor();
        Select();
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
}