using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;

public class ObjectSelectionMenu : MonoBehaviour
{
    public GameManager _gameManager;
    public GameObject[] objectsToPlace;
    public Transform placeholder; 
    public Transform obstacleParent; 
    public Button[] buttons; 
    
    private GameObject currentObject;
    private GameObject inWorldObject;
    private Coroutine currentHighlightCoroutine;
    private int selectedIndex = 0; 
    private bool objectPlaced = false;
   

    public GameObject menuPanel; 
    private void Start()
    {
        if (buttons.Length == 0)
        {
            buttons = GetComponentsInChildren<Button>();
        }

        HighlightButton(buttons[selectedIndex]);
    }

    private void Update()
    {
        if (!gameObject.activeSelf) return;

        if (!objectPlaced)
        {
            HandleCursorSelection();
        }
        CheckForSelectionInput();
        
        if (currentObject != null)
        {
            currentObject.transform.position = placeholder.position;
        }
    }

    private void HandleCursorSelection()
    {
        if (Gamepad.current != null)
        {
            if (Gamepad.current.dpad.up.wasPressedThisFrame)
            {
                selectedIndex = (selectedIndex - 1 + buttons.Length) % buttons.Length;
                HighlightButton(buttons[selectedIndex]);
            }
            if (Gamepad.current.dpad.down.wasPressedThisFrame)
            {
                selectedIndex = (selectedIndex + 1) % buttons.Length;
                HighlightButton(buttons[selectedIndex]);
            }
        }
    }


    private void HighlightButton(Button button)
    {
        ResetButtonColors();

        if (currentHighlightCoroutine != null)
        {
            StopCoroutine(currentHighlightCoroutine);
        }
        currentHighlightCoroutine = StartCoroutine(SmoothHighlight(button));
    }

    private void ResetButtonColors()
    {

        Color originalColor = Color.white;
        foreach (var btn in buttons)
        {
            btn.GetComponent<Image>().color = originalColor;
        }
    }

    private IEnumerator SmoothHighlight(Button button)
    {
        Color originalColor = button.GetComponent<Image>().color;
        Color highlightColor = Color.yellow;

        float duration = 0.3f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            button.GetComponent<Image>().color = Color.Lerp(originalColor, highlightColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        button.GetComponent<Image>().color = highlightColor;
        currentHighlightCoroutine = null;
    }

    private void CheckForSelectionInput()
    {
        if (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            if (!objectPlaced)
            {
                PlaceObjectAtCursor();
                StartCoroutine(ButtonPressAnimation(buttons[selectedIndex]));
            }
            else
            {
                PlaceObjectInWorld();
            }
        }
    }

    private void PlaceObjectInWorld()
    {
        var worldPos = GetCursorWorldPosition();
        inWorldObject = Instantiate(objectsToPlace[selectedIndex], placeholder.position, Quaternion.identity, obstacleParent);
        inWorldObject.transform.localScale *= 10;//edit here to get correct size
        Destroy(currentObject);
        
        _gameManager.EndPlacementPhase();
    }

    private void PlaceObjectAtCursor()
    {
        currentObject = Instantiate(objectsToPlace[selectedIndex], placeholder.position, Quaternion.identity, placeholder);
        currentObject.transform.localScale *= 100;
        objectPlaced = true;
        
        _gameManager.EndSelectionPhase();
        
    }


    private Vector3 GetCursorWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
    }

    private IEnumerator ButtonPressAnimation(Button button)
    {
        Vector3 originalScale = button.transform.localScale;

        //scale down
        button.transform.localScale = originalScale * 0.9f; 
        yield return new WaitForSeconds(0.1f);

        //scale up
        button.transform.localScale = originalScale;
    }
}