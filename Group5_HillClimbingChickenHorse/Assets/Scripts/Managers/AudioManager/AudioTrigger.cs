using System.Threading.Tasks;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]private bool hasBeenTriggered = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource component found on this GameObject.");
        }
    }

    async private void  OnCollisionEnter2D(Collision2D col)
    {
        if (!hasBeenTriggered && col.collider.CompareTag("Wheel"))
        {
            hasBeenTriggered = true;
            PlayAudio();
            await WaitOneSecond(); 
            hasBeenTriggered = false;
        }
    }
    
    async private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasBeenTriggered && other.CompareTag("Wheel"))
        {
            hasBeenTriggered = true;
            PlayAudio();
            await WaitOneSecond(); 
            hasBeenTriggered = false;
        }
    }
    private async Task WaitOneSecond()
    {
        await Task.Delay(1000); 
    }

     private void OnCollisionExit2D(Collision2D col)
        {
       
            if (!hasBeenTriggered && col.collider.CompareTag("Wheel"))
            {
                hasBeenTriggered = false;   
                stopAudio();
            }
        }
    private void PlayAudio()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource is null");
        }
    }

    private void stopAudio()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
        else
        {
            Debug.LogWarning("Audio is null");
        }
    }
}