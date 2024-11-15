using UnityEngine;

public class PlacementChecker : MonoBehaviour
{
    private bool isGrounded = false; // Check if the object is on the ground

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("Object touched the ground");
        }
    }

    public void LockPlacement()
    {
        if (isGrounded)
        {
            Debug.Log("Placement Locked");
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Static; // Make the object static
            }
        }
        else
        {
            Debug.LogWarning("Cannot lock placement - object not on ground!");
        }
    }
}