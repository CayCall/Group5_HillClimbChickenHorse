using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("Selection Phase")]
    public GameObject pnlItems;
    public GameObject[] items;
    private int selectedIndex = 0; 
    private GameObject instantiatedItem; 
    
    [Header("Placement Phase")]
    public Transform uiParent; 
    public Camera mainCamera; // Reference to the main camera
    public float normalZoom = 5f; // Default camera zoom size
    public float placementZoom = 10f; // Zoom size during placement phase
    public float zoomSpeed = 2f; // Speed of zooming

    // Deactivate player input 
    [SerializeField] private PlayerInput[] _playerInputsArray;
    [SerializeField] private PlayerInputs[] _playerInputArray;

    private bool isPlacementLocked = false; 

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
        pnlItems.SetActive(false); // Hide the item selection panel
        Debug.Log("Selection Phase Ended");
        StartPlacementPhase(); // Begin placement phase
    }
    private void StartPlacementPhase()
    {
        Debug.Log("Placement Phase Started");
        HandlePlayerActiveState();
        isPlacementLocked = false;
        
        StartCoroutine(AdjustCameraZoom(placementZoom));
    }

    private void EndPlacementPhase()
    {
        Debug.Log("Placement Phase Ended");

        if (instantiatedItem != null)
        {
            // Lock the object in place
            instantiatedItem.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static; 
        }

        // Smoothly zoom back in the camera
        StartCoroutine(AdjustCameraZoom(normalZoom));

        HandlePlayerDeactivateState();
        // Add logic to transition to the next phase or reset selection
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

    private IEnumerator AdjustCameraZoom(float targetZoom)
    {
        float currentZoom = mainCamera.orthographicSize;

        while (Mathf.Abs(currentZoom - targetZoom) > 0.01f)
        {
            currentZoom = Mathf.Lerp(currentZoom, targetZoom, zoomSpeed * Time.deltaTime);
            mainCamera.orthographicSize = currentZoom;
            yield return null;
        }

        mainCamera.orthographicSize = targetZoom;
    }
}
