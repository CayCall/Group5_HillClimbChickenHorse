using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [Header("Selection Phase")]
    public GameObject pnlItems;
    public GameObject[] items;
    private int selectedIndex = 0; 
    private GameObject instantiatedItem; 
    
    [Header("Placement Phase")]
    public Transform uiParent; // Cursor as parent for object to spawn on cursor

    // Deactivate player input 
    [SerializeField] private PlayerInput[] _playerInputsArray;
    [SerializeField] private PlayerInputs[] _playerInputArray;

    private bool isPlacementLocked = false; // Flag to prevent multiple placements

    private void Start()
    {
        StartSelectionPhase();
    }

    private void StartSelectionPhase()
    {
        pnlItems.SetActive(true); 
        Debug.Log("Selection Phase Started");
        HandlePlayerDeactivateState();
    }

    private void EndSelectionPhase()
    {
        pnlItems.SetActive(false); 
        Debug.Log("Selection Phase Ended");
        StartPlacementPhase();
    }

    private void StartPlacementPhase()
    {
        Debug.Log("Placement Phase Started");
        HandlePlayerActiveState();
        isPlacementLocked = false; // Reset placement lock for the new object
    }

    private void EndPlacementPhase()
    {
        Debug.Log("Placement Phase Ended");

        if (instantiatedItem != null)
        {
            // Ensure the item is locked in place
            instantiatedItem.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static; 
        }

        HandlePlayerDeactivateState();
        // Add logic to transition to the next phase or reset selection
    }

    public void HandleInput(PlayerInputs playerInputs)
    {
        if (pnlItems.activeSelf)
        {
            HandleSelectionInput(playerInputs);
        }
        else if (instantiatedItem != null && !isPlacementLocked)
        {
            HandlePlacementInput(playerInputs);
        }
    }

    private void HandleSelectionInput(PlayerInputs playerInputs)
    {
        if (playerInputs.NavigateDown())
        {
            selectedIndex = (selectedIndex + 1) % items.Length;
            Debug.Log($"Selected Item Index: {selectedIndex}");
        }
        
        if (playerInputs.SelectItem())
        {
            Debug.Log($"Item {items[selectedIndex].name} selected!");
            if (instantiatedItem != null)
            {
                Destroy(instantiatedItem);
            }

            instantiatedItem = Instantiate(items[selectedIndex], uiParent);
            instantiatedItem.transform.localPosition = Vector3.zero; // Center it in UI space
            instantiatedItem.transform.localScale = Vector3.one;

            EndSelectionPhase();
        }
    }

    private void HandlePlacementInput(PlayerInputs playerInputs)
    {
        // Place the object when the player presses the confirm button
        if (playerInputs.ConfirmPlacement())
        {
            Debug.Log("Placement Confirmed");
            isPlacementLocked = true; // Lock placement input

            // Lock the object in place
            if (instantiatedItem != null)
            {
                instantiatedItem.GetComponent<PlacementChecker>().LockPlacement();
                EndPlacementPhase();
            }
        }
    }

    private void HandlePlayerDeactivateState()
    {
        for (int i = 0; i < _playerInputsArray.Length; i++)
        {
            _playerInputsArray[i].enabled = false;
        }
        for (int i = 0; i < _playerInputArray.Length; i++)
        {
            _playerInputArray[i].enabled = false;
        }    
    }

    private void HandlePlayerActiveState()
    {
        for (int i = 0; i < _playerInputsArray.Length; i++)
        {
            _playerInputsArray[i].enabled = true;
        }
        for (int i = 0; i < _playerInputArray.Length; i++)
        {
            _playerInputArray[i].enabled = true;
        }        
    }
}
