using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputs : MonoBehaviour
{
    public Vector2 cursorMoveDirction;
    private void Update()
    {
        Gas();
    }

    public void Gas()
    {
        float positive = 0f;
        float negative = 0f;
        if (Gamepad.current.enabled)
        {
            positive = Gamepad.current.rightTrigger.ReadValue(); 
            negative = Gamepad.current.leftTrigger.ReadValue();
        }
        else
        {
            positive = Keyboard.current.rightArrowKey.ReadValue();
            negative = Keyboard.current.leftArrowKey.ReadValue();
        }

        var direction = positive - negative;
        Debug.Log("Direction is " + direction);
    }
    public void Cursor()
    {
        cursorMoveDirction = Gamepad.current.leftStick.ReadValue();
        
        Debug.Log("curser moves in the direction " + cursorMoveDirction);
    }
    
    public void Select()
    {
        var select = Gamepad.current.buttonSouth.isPressed;

        if (select)
        {
            Debug.Log("Select button was pressed");
        }
    }
}
