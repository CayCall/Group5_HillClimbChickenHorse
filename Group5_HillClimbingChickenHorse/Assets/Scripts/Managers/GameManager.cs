using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [Header("Selection Phase")]
    public GameObject pnlItems;
    public GameObject[] items;
    private int selectedIndex = 0; 
    private GameObject instantiatedItem; 
    
    [Header("Placement Phase")]
    public Transform uiParent; 
    public CinemachineVirtualCamera Camera;
    public float normalZoom = 5f; // Default camera zoom size
    public float placementZoom = 100f; // Zoom size during placement phase
    public float zoomSpeed = 2f; // Speed of zooming

    // Deactivate player input 
    [SerializeField] private PlayerInput[] _playerInputsArray;
    [SerializeField] private PlayerInputs[] _playerInputArray;

    private bool isPlacementLocked = false; 
    
    //for zooming placements
    [Header("Camera Targets")]
    public Transform player; // Reference to the player's position
    public Vector3 mapCenter; // Center position of the map

    private void Start()
    {
        // Ensure the camera starts at the normal zoom level
        Camera.m_Lens.OrthographicSize = normalZoom;

        // Begin the selection phase
        StartSelectionPhase();
    }

    private void Update()
    {
        if (Keyboard.current.zKey.wasPressedThisFrame) // Example: Zoom out with 'Z'
        {
            ZoomOutToPlacement();
        }

        if (Keyboard.current.xKey.wasPressedThisFrame) // Example: Zoom in with 'X'
        {
            ZoomInToNormal();
        }
    }
    

    private void StartSelectionPhase()
    {
        pnlItems.SetActive(true); 
        Debug.Log("Selection Phase Started");
        HandlePlayerDeactivateState();
    }

    public void EndSelectionPhase()
    {
        pnlItems.SetActive(false); // Hide the item selection panel
        Debug.Log("Selection Phase Ended");
        StartPlacementPhase(); // Begin placement phase
    }
    private void StartPlacementPhase()
    {
        Debug.Log("Placement Phase Started");
        isPlacementLocked = false;

        // Start zooming out to placement zoom
        ZoomOutToPlacement(); // Zoom out to map 
    }
    //after objejct is placed
    public void EndPlacementPhase()
    {
        Debug.Log("Placement Phase Ended");

        if (instantiatedItem != null)
        {
            instantiatedItem.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }

        ZoomInToNormal(); // Zoom back to the player
        HandlePlayerActiveState();
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


private IEnumerator AdjustCameraZoom(float targetZoom, Vector3 targetPosition)
{
    float currentZoom = Camera.m_Lens.OrthographicSize;
    Vector3 currentPosition = Camera.transform.position;

    while (Mathf.Abs(currentZoom - targetZoom) > 0.01f || Vector3.Distance(currentPosition, targetPosition) > 0.01f)
    {
        // Smoothly interpolate zoom
        currentZoom = Mathf.Lerp(currentZoom, targetZoom, zoomSpeed * Time.deltaTime);
        Camera.m_Lens.OrthographicSize = currentZoom;

        // Smoothly interpolate position
        currentPosition = Vector3.Lerp(currentPosition, targetPosition, zoomSpeed * Time.deltaTime);
        Camera.transform.position = currentPosition;

        yield return null;
    }

    // Ensure final values are set precisely
    Camera.m_Lens.OrthographicSize = targetZoom;
    Camera.transform.position = targetPosition;
}

public void ZoomInToNormal()
{
    StartCoroutine(AdjustCameraZoom(normalZoom, player.position));
}

public void ZoomOutToPlacement()
{
    Vector3 mapPosition = new Vector3(mapCenter.x, mapCenter.y, Camera.transform.position.z);
    StartCoroutine(AdjustCameraZoom(placementZoom, mapPosition));
}
// game loop 
}
