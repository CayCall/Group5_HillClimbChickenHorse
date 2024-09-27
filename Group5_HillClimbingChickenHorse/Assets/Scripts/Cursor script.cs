using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Cursor_script : MonoBehaviour
    {
        [SerializeField] private PlayerInputs playerInputs;
        [SerializeField] private Transform cursorObject;
        [SerializeField] private float moveSpeed;

        private void Update()
        {
            MoveCursor();
        }

        public void MoveCursor()
        {
            var newPos = new Vector2((cursorObject.position.x 
                                      + playerInputs.cursorMoveDirction.x) 
                                     * moveSpeed, 
                                    (cursorObject.position.y 
                                     + playerInputs.cursorMoveDirction.y) 
                                    * moveSpeed);
            cursorObject.position = newPos;
        }
    }
}