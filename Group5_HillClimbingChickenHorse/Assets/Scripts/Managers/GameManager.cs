using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Cinemachine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Selection Phase")]
    public GameObject pnlItems;

    public ObjectSelectionMenu Uicanvas;
    public GameObject[] items;
    private int selectedIndex = 0; 
    private GameObject instantiatedItem;

    
    [Header("Placement Phase")]
    public Transform uiParent; 
    public CinemachineVirtualCamera Camera;
    public float normalZoom = 1f; // Default camera zoom size
    public float placementZoom = 100f; // Zoom size during placement phase
    public float zoomSpeed = 1f; // Speed of zooming
    public GameObject placementText;
    public GameObject Cursor;

    // Deactivate player input 
    [SerializeField] private PlayerInput[] _playerInputsArray;
    [SerializeField] private PlayerInputs[] _playerInputArray;

    private bool isPlacementLocked = false; 
    
    //for zooming placements
    [Header("Camera Targets")]
    public Transform player; // Reference to the player's position
    public Transform MapTransform; // Reference to the map's Transform
    private void Start()
    {
        Cursor.SetActive(true);
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
    

    public void StartSelectionPhase()
    {
        pnlItems.SetActive(true);
        Cursor.SetActive(true);
        var selectionMenu = Uicanvas.GetComponent<ObjectSelectionMenu>();
        if (selectionMenu != null)
        {
            selectionMenu.ResetMenuState();
        }
        Debug.Log("Selection Phase Started");
        
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
        
        // hide placement text
        placementText.SetActive(false);
        //hide cursor
        Cursor.SetActive(false);
        
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
        foreach (var playerInput in _playerInputsArray)
        {
            if (playerInput != null)
            {
                playerInput.enabled = true;
            }
        }

        foreach (var customInput in _playerInputArray)
        {
            if (customInput != null)
            {
                customInput.enabled = true;
            }
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
    if (MapTransform == null)
    {
        Debug.LogError("MapTransform is not assigned!");
        return;
    }

    Vector3 mapPosition = new Vector3(MapTransform.position.x, MapTransform.position.y, Camera.transform.position.z);
    Debug.Log($"Zooming out to MapTransform at position: {mapPosition}");
    StartCoroutine(AdjustCameraZoom(placementZoom, mapPosition));
}
// game loop 
}
