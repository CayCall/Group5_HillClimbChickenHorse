using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainWheelDistance : MonoBehaviour
{
    [SerializeField] private Transform wheelOne;
    [SerializeField] private Transform wheelTwo;
    [SerializeField] private float fixedDistance = 2f; // Desired distance between the two wheels

    void Update()
    {
        MaintainFixedDistance();
    }

    private void MaintainFixedDistance()
    {
        // Calculate the current distance between the wheels
        Vector3 direction = wheelOne.position - wheelTwo.position;
        float currentDistance = direction.magnitude;

        // Calculate the difference between the current distance and the desired fixed distance
        float distanceDifference = currentDistance - fixedDistance;

        // If the distance is not the same as the fixed distance, adjust positions
        if (Mathf.Abs(distanceDifference) > Mathf.Epsilon) // Avoid floating point precision errors
        {
            // Normalize the direction vector (unit vector)
            direction = direction.normalized;

            // Move both wheels half of the distance difference in opposite directions
            Vector3 offset = direction * (distanceDifference / 2);

            // Adjust the positions to maintain the fixed distance
            wheelOne.position -= offset; // Move wheelOne closer
            wheelTwo.position += offset; // Move wheelTwo farther
        }
    }
}
