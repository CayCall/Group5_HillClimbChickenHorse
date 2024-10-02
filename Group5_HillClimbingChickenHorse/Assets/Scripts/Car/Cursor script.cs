using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class Cursor_script : MonoBehaviour
    {
        public InputAction moveAction; // Reference to your move action (e.g., left stick)
        public InputAction clickAction; // Reference to your click action (e.g., A button)
        public RectTransform cursor; // The UI element or object you want to control as a cursor
        public float cursorSpeed = 1000f; // Cursor speed multiplier

        private Vector2 moveInput; // Store movement input from the controller
        private Camera mainCamera;

        private void Start()
        {
            // Get the main camera
            mainCamera = Camera.main;

            if (mainCamera == null)
            {
                Debug.LogError("Main Camera not found!");
            }
        }

        private void OnEnable()
        {
            moveAction.Enable();
            clickAction.Enable();
        }

        private void OnDisable()
        {
            moveAction.Disable();
            clickAction.Disable();
        }

        void Update()
        {
            if (mainCamera != null)
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

                // Check for click input
                if (clickAction.triggered)
                {
                    HandleClick();
                }
            }
            else
            {
                Debug.LogError("Main Camera is not set!");
            }
        }

        private void HandleClick()
        {
            // Step 1: Convert the cursor position (UI space) to world position using the main camera
            Vector3 screenPosition = cursor.position;  // Screen space position (UI space)
    
            // Ensure Z is set to 0 (or wherever your 2D game objects are located on the Z-plane)
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 0));

            // Debug the cursor position in both screen and world space
            Debug.Log($"Cursor screen position: {screenPosition}, Adjusted world position: {worldPosition}");

            // Step 2: Check for UI interactions first
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = screenPosition // Use screen position for UI interaction
            };

            var raycastResults = new System.Collections.Generic.List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, raycastResults);

            Debug.Log($"UI Raycast results count: {raycastResults.Count}");

            if (raycastResults.Count > 0)
            {
                Debug.Log("Clicked on UI element: " + raycastResults[0].gameObject.name);
                // Handle UI element interaction here
            }
            else
            {
                // Step 3: If no UI was hit, check for 2D game object interactions
                RaycastHit2D hit = Physics2D.Raycast(screenPosition, Vector2.zero);

                if (hit.collider != null)
                {
                    Debug.Log("Hit 2D object: " + hit.collider.gameObject.name + " at position: " + hit.point);
                    // Handle 2D object interaction here
                }
                else
                {
                    Debug.Log("No 2D object hit, clicked on empty space.");
                }
            }
        }
    }
}
