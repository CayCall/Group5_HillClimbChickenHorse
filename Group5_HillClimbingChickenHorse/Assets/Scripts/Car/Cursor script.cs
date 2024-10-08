using System;
using UnityEngine;

namespace DefaultNamespace
{
   public class Cursor_script : MonoBehaviour
    {
        [SerializeField] private PlayerInputs playerInputs; // Assuming you have a PlayerInputs script handling controller input
        [SerializeField] public Transform cursorObject;
        [SerializeField] private float moveSpeed;
        
        private GameObject selectedObject;
        private bool isObjectSelected = false;

        private void Update()
        {
            MoveCursor();
           // CheckObjectSelection();
        }

        public void MoveCursor()
        {
            if (cursorObject != null)
            {
                var newPos = new Vector2((cursorObject.position.x
                                          + playerInputs.cursorMoveDirction.x)
                                         * moveSpeed,
                    (cursorObject.position.y
                     + playerInputs.cursorMoveDirction.y)
                    * moveSpeed);
                cursorObject.position = newPos;
            }
            
            if (isObjectSelected && selectedObject != null)
            {
                selectedObject.transform.position = cursorObject.position;
            }
        }

        
    }
}