using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class Cursor_script : MonoBehaviour
    {
        public InputAction moveAction; // Reference to your move action (e.g., left stick)
        public RectTransform cursor; // The UI element or object you want to control as a cursor
        public float cursorSpeed = 1000f; // Cursor speed multiplier

        private Vector2 moveInput; // Store movement input from the controller

        private void OnEnable()
        {
            moveAction.Enable();
        }

        private void OnDisable()
        {
            moveAction.Disable();
        }

        void Update()
        {
            // Get the input from the controller stick
            moveInput = moveAction.ReadValue<Vector2>();

            // Calculate the new cursor position
            Vector2 newCursorPos = cursor.anchoredPosition + (moveInput * cursorSpeed * Time.deltaTime);

            // Clamp the cursor position within the screen bounds
            newCursorPos.x = Mathf.Clamp(newCursorPos.x, -Screen.width / 2, Screen.width / 2);
            newCursorPos.y = Mathf.Clamp(newCursorPos.y, -Screen.height / 2, Screen.height / 2);

            // Update the position of the cursor
            cursor.anchoredPosition = newCursorPos;
        }
    }
}
