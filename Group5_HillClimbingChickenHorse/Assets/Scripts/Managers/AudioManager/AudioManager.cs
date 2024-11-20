using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [System.Serializable]
    public class AudioClipEntry
    {
        public string identifier;
        public AudioClip clip;
    }

    [Header("Clips")]
    public List<AudioClipEntry> audioClips;
    
    [Header("")]
    [Header("Audio Volume")]
    public float audioVolume = 0.25f;

    private Dictionary<string, AudioClip> audioClipDictionary;

    public UnityEvent onAudioComplete; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        if (onAudioComplete == null)
        {
            onAudioComplete = new UnityEvent();
        }

        audioClipDictionary = new Dictionary<string, AudioClip>();

        foreach (AudioClipEntry entry in audioClips)
        {
            if (!audioClipDictionary.ContainsKey(entry.identifier))
            {
                audioClipDictionary.Add(entry.identifier, entry.clip);
            }
        }
    }

    public void PlayAudio(string identifier)
    {
        if (audioClipDictionary.ContainsKey(identifier))
        {
            AudioSource.PlayClipAtPoint(audioClipDictionary[identifier], Camera.main.transform.position, audioVolume);
            Debug.Log("Played audio: " + identifier);
            StartCoroutine(WaitForAudioToEnd(audioClipDictionary[identifier].length));
        }
        else
        {
            Debug.LogWarning("Audio identifier not found: " + identifier);
        }
    }

    private IEnumerator WaitForAudioToEnd(float duration)
    {
        yield return new WaitForSeconds(duration);
        onAudioComplete?.Invoke(); 
    }
}