using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAirRotate : MonoBehaviour
{
    public PlayerInputs pI;
    public float speed;
    public GameObject wheel1;
    public GameObject wheel2;
    public GameObject centerObject;

    private void Start()
    {
        pI = GetComponentInChildren<PlayerInputs>();
    }

    void Update()
    {
        // Ensure the inputValue is between -1 and 1
        pI.throttle = Mathf.Clamp(pI.throttle, -1f, 1f);

        // If there's an input value, rotate the wheels around the center object
        if (!wheel1.GetComponent<CharacterController>().isGrounded && !wheel1.GetComponent<CharacterController>().isGrounded)
        {
            RotateWheels(pI.throttle);
            Debug.Log("Off The Ground!!");
        }
    }

    void RotateWheels(float direction)
    {
        // Calculate the rotation speed based on the input value and direction
        float rotationSpeed = direction * speed * Time.deltaTime;

        // Rotate wheel1 around the center object
        wheel1.transform.RotateAround(centerObject.transform.position, Vector3.forward, rotationSpeed);

        // Rotate wheel2 around the center object
        wheel2.transform.RotateAround(centerObject.transform.position, Vector3.forward, rotationSpeed);
    }
}
