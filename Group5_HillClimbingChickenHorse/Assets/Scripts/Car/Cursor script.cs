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
            CheckObjectSelection();
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

        public void CheckObjectSelection()
        {
            // gonna use 'A or  X' button (or another button)for selecting the object
            if (cursorObject != null && Input.GetButtonDown("SelectButton")) 
            {
                RaycastHit2D hit = Physics2D.Raycast(cursorObject.position, Vector2.zero);

                if (hit.collider != null && hit.collider.CompareTag("Selectable"))
                {
                    Debug.Log("hit:" + gameObject.name);
                    selectedObject = hit.collider.gameObject;
                    isObjectSelected = true;
                    selectedObject.GetComponent<Collider2D>().enabled = false;
                    
                    FindObjectOfType<GameManager>().ClosePanel();
                }
            }
        }

        public void InstantiateSelectedObject(Vector2 position)
        {
            if (selectedObject != null)
            {
                Instantiate(selectedObject, position, Quaternion.identity);
                Destroy(selectedObject);
                isObjectSelected = false;
                selectedObject = null;
            }
        }
    }
}