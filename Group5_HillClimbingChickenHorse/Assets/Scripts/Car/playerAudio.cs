using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAudio : MonoBehaviour
{
    [Header("Gas audio")]
    public PlayerInputs playerInputs; 
    public AudioSource engineAudio;  
    public float minPitch = 0.8f;   
    public float maxPitch = 2f;    

    void Start()
    {
        if (engineAudio == null)
        {
            engineAudio = GetComponent<AudioSource>();
        }

        if (playerInputs == null)
        {
            Debug.LogError("PlayerInputs reference is missing.");
        }
    }

    void Update()
    {
        if (playerInputs != null && engineAudio != null)
        {
            // Map throttle value to pitch range
            float throttle = Mathf.Clamp01(playerInputs.throttle); // Ensure throttle is between 0 and 1
            engineAudio.pitch = Mathf.Lerp(minPitch, maxPitch, throttle);
        }
    }
}