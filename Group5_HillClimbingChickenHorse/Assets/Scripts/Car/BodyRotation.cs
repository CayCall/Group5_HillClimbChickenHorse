using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyRotation : MonoBehaviour
{
    public Transform wheel1; // Reference to Wheel 1
    public Transform wheel2; // Reference to Wheel 2
    public Transform body; // Reference to the Body

    [Range(-180f, 180f)] public float fineTuneAngle = 0f; // Angle adjustment for fine-tuning

    void Update()
    {
        if (wheel1 == null || wheel2 == null || body == null)
        {
            Debug.LogWarning("Please assign all required references (wheel1, wheel2, body).");
            return;
        }

        // Calculate the direction from wheel1 to wheel2
        Vector2 direction = wheel2.position - wheel1.position;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Adjust the body's rotation
        body.rotation = Quaternion.Euler(0, 0, angle + fineTuneAngle);
    }
}
