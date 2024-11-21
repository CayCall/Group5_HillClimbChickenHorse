using UnityEngine;

public class WheelRaycastSnap2D : MonoBehaviour
{
    public float rayDistance = 0.6f;   // Distance for each raycast
    public float snapHeight = 0.5f;    // Height to snap above the hit point
    public int numberOfRays = 8;       // Number of rays to cast, editable in Inspector
    public float radius = 0.3f;        // Radius of the circle around the object from which rays are cast
    public float maxAngle = 45f;       // Maximum angle from the downward direction
    public float centralRayAngle = 0f; // Angle of the central ray in degrees

    private Rigidbody2D rb2D;          // Reference to the Rigidbody2D component

    void Start()
    {
        // Get the Rigidbody2D component attached to the object
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Store the lowest point hit by the rays
        float lowestY = float.MaxValue; // Start with the highest possible value
        bool hitDetected = false;         // Flag to check if any hit is detected

        // Calculate the angular step based on the max angle and the number of rays
        float angleStep = maxAngle * 2f / (numberOfRays - 1);
        float startAngle = centralRayAngle - maxAngle; // Set the starting angle based on central ray angle

        // Loop through the number of rays, casting each ray in a circular pattern
        for (int i = 0; i < numberOfRays; i++)
        {
            // Calculate the angle for this ray (convert to radians)
            float angle = (startAngle + i * angleStep) * Mathf.Deg2Rad;

            // Calculate the direction of the ray using trigonometry
            Vector2 rayDirection = new Vector2(Mathf.Sin(angle), -Mathf.Cos(angle));

            // Create the ray starting from the object's position
            Vector2 rayOrigin = (Vector2)transform.position + rayDirection * radius;

            // Perform the raycast in 2D space
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);

            if (hit.collider != null)
            {
                hitDetected = true;

                // Calculate the distance from the center of the wheel to the hit point
                float distanceFromCenter = Vector2.Distance(transform.position, hit.point);

                // Check if the hit point is within the snap range
                if (distanceFromCenter <= 0.5f)
                {
                    // If the hit is below the lowest point, store the lowest point
                    if (hit.point.y < lowestY)
                    {
                        lowestY = hit.point.y;
                    }
                }

                // Optional: Debugging the raycasts
                Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.green);
            }
            else
            {
                // Optional: Visualize the rays that didn't hit anything
                Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);
            }
        }

        // If at least one ray hit and the lowest point is within the snap range, adjust the object's height
        if (hitDetected && lowestY < float.MaxValue)
        {
            Vector2 newPosition = new Vector2(transform.position.x, lowestY + snapHeight);
            transform.position = newPosition;

            // Turn off the gravity for the Rigidbody2D once snapped
            if (rb2D != null)
            {
                rb2D.gravityScale = 0f;
                rb2D.velocity = Vector2.zero;
            }
        }
        else
        {
            // Turn gravity back on if the wheel is above 0.5 units from the lowest detected point
            if (rb2D != null)
            {
                rb2D.gravityScale = 1f;
            } // Set to your desired gravity scale
        }
    }
    
    // find the angle first 
}
