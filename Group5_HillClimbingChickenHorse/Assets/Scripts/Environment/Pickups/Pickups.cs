using System;
using System.Collections;
using System.Collections.Generic;
using CC;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    [SerializeField] private CharacterControlerBasic[] characterControlerBasic;
    
    public float extraSpeed = 2f;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("ExtraSpeed"))
        {
           
            for (int i = 0; i < characterControlerBasic.Length; i++)
            {
                characterControlerBasic[i].speed += extraSpeed;
                
                Debug.Log("New Speed for Character " + i + ": " + characterControlerBasic[i].speed);
            }
            Destroy(col.gameObject);
        }
    }
}