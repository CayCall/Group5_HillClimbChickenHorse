using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidpointPositioner : MonoBehaviour
{
    [SerializeField] private Transform objectA;  // First object (use Transform to access its position)
    [SerializeField] private Transform objectB;  // Second object (use Transform to access its position)
    [SerializeField] private GameObject midpointObject;  // Object to place in the middle

    // Update is called once per frame
    void Update()
    {
        // Get the world positions of the child objects
        Vector3 positionA = objectA.position;
        Vector3 positionB = objectB.position;

        // Calculate the midpoint between objectA and objectB
        Vector3 midpoint = new Vector3(
            (positionA.x + positionB.x) / 2,
            (positionA.y + positionB.y) / 2,
            (positionA.z + positionB.z) / 2
        );

        // Set the position of the midpointObject to the calculated midpoint
        midpointObject.transform.position = midpoint;
    }
}