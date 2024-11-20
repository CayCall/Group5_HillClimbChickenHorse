using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [System.Serializable]
    public class AudioClipEntry
    {
        public string identifier;
        public AudioClip clip;
    }

    [Header("Audio Clips")]
    public List<AudioClipEntry> audioClips;

    [Header("Audio Settings")]
    public float audioVolume = 0.5f;

    private Dictionary<string, AudioClip> audioClipDictionary;

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

        audioClipDictionary = new Dictionary<string, AudioClip>();
        foreach (AudioClipEntry entry in audioClips)
        {
            if (!audioClipDictionary.ContainsKey(entry.identifier))
            {
                audioClipDictionary.Add(entry.identifier, entry.clip);
            }
        }
    }

    public void PlayAudio(string identifier, Vector3 position)
    {
        if (audioClipDictionary.ContainsKey(identifier))
        {
            AudioSource.PlayClipAtPoint(audioClipDictionary[identifier], position, audioVolume);
        }
        else
        {
            Debug.LogWarning("Audio identifier not found: " + identifier);
        }
    }
}
