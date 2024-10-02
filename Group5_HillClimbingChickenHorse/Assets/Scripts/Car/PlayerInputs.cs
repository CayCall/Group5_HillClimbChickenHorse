using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    public Vector2 cursorMoveDirction;
    public float throttle;
    public bool useKeyboard;

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
        float positive = 0f;
        float negative = 0f;

        if (Gamepad.current != null && Gamepad.current.enabled)  // Ensure gamepad is connected
        {
            positive = Gamepad.current.rightTrigger.ReadValue(); 
            negative = Gamepad.current.leftTrigger.ReadValue();
            Debug.Log("cont connected");
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
}