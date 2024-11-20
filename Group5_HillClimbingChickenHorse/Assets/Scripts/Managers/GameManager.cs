using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Gminstance;
    [Header("General")]
    public GameObject cursor;
    // Deactivate player input 
    [SerializeField] private PlayerInput[] _playerInputsArray;
    [SerializeField] private PlayerInputs[] _playerInputArray;
    private bool isPlacementLocked = false; 
    
    [Header("")]
    [Header("Selection Phase")]
    public GameObject pnlItems;
    public GameObject[] items;
    private int selectedIndex = 0; 
    private GameObject instantiatedItem; 
    
    [Header("")]
    [Header("Placement Phase")]
    public Transform uiParent; 
    public CinemachineVirtualCamera Camera;
    public float normalZoom = 30f; 
    public float placementZoom = 100f; 
    public float zoomSpeed = 2f; 
    
    [Header("")]
    //zooming
    [Header("Camera Targets")]
    public Transform player;
    public Vector3 mapCenter;

    private void Awake()
    {
        if ( Gminstance == null)
        {
            Gminstance= this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        cursor.SetActive(true);
        // camera starts at the normal zoom level
        Camera.m_Lens.OrthographicSize = normalZoom;
        
        StartSelectionPhase();
        
    }
    

    private void StartSelectionPhase()
    {
        pnlItems.SetActive(true); 
        Debug.Log("Selection Phase Started");
        HandlePlayerDeactivateState();
    }
    public void EndSelectionPhase()
    {
        pnlItems.SetActive(false);
        Debug.Log("Selection Phase Ended");
        StartPlacementPhase(); 
    }
    private void StartPlacementPhase()
    {
        Debug.Log("Placement Phase Started");
        isPlacementLocked = false;
        ZoomOutToPlacement(); 
    }
    //after objejct is placed
    public void EndPlacementPhase()
    {
        Debug.Log("Placement Phase Ended");

        if (instantiatedItem != null)
        {
            instantiatedItem.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }

        ZoomInToNormal();
        cursor.SetActive(false);
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
            currentZoom = Mathf.Lerp(currentZoom, targetZoom, zoomSpeed * Time.deltaTime);
            Camera.m_Lens.OrthographicSize = currentZoom;
        
            currentPosition = Vector3.Lerp(currentPosition, targetPosition, zoomSpeed * Time.deltaTime);
            Camera.transform.position = currentPosition;

            yield return null;
        }

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
