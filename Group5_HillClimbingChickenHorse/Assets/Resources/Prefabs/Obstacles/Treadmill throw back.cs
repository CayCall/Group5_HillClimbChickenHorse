using System.Collections;
using System.Collections.Generic;
using CC;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Treadmillthrowback : MonoBehaviour
{
    public Vector2 forceDirection;
    
    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Wheel"))
        {
            GameObject wheel = col.gameObject;
            CharacterControlerBasic ccb = wheel.GetComponent<CharacterControlerBasic>();
            ccb.rB2D.AddForce(forceDirection);
        }
    }
}
