using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFollow : MonoBehaviour
{
    public Transform player; 
    public Vector3 offset; 

    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.position + offset; 
          
        }
    }
}
