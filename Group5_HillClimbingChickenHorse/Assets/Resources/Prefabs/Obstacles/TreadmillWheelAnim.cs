using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreadmillWheelAnim : MonoBehaviour
{
    [SerializeField] private float timeForFullRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float rotationSpeed = 360f / timeForFullRotation;
        transform.RotateAround(transform.position, Vector3.back, rotationSpeed * Time.deltaTime);
    }
}
