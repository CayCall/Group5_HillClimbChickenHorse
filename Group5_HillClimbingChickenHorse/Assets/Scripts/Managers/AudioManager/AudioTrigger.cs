using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
  
    public string audioIdentifier;
    public AudioTrigger nextTrigger;

    private bool hasBeenTriggered = false;

    private void OnEnable()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.onAudioComplete.AddListener(OnAudioComplete);
        }
        else
        {
            Debug.LogError("AudioManager instance is null.");
        }
    }

    private void OnDisable()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.onAudioComplete.RemoveListener(OnAudioComplete);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enetered");
        if (!hasBeenTriggered && other.CompareTag("1layer"))
        {
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlayAudio(audioIdentifier);
                hasBeenTriggered = true;
                DisableTrigger();
            }
            else
            {
                Debug.LogError("AudioManager instance is null.");
            }
        }
    }

    private void DisableTrigger()
    {
        GetComponent<Collider>().enabled = false;
    }

    private void OnAudioComplete()
    {
        if (nextTrigger != null)
        {
            nextTrigger.gameObject.SetActive(true);
        }
        gameObject.SetActive(false);
    }
}