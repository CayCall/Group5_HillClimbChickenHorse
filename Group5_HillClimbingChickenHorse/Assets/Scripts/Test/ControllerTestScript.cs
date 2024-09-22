using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTestScript : MonoBehaviour
{
   public GameObject ItemMenu;
   [SerializeField] private bool isMenuopen = false;

   // Update is called once per frame
    void Update()
    {
       // activatePanel();    
    }


    
     //CHECK IF PANEL IS CURRENTLY OPEN, IF IS OPEN PRESS BUTTON TO OPEN MENU THEN USE X BUTTON TO 0PEN AND CLOSE THE SAME MENU
     // IF IS OPEN THE BOOL SHOULD BE TRUE AND  PANEL MUST OPEN ELSE THE OPPOSITE

     /*void activatePanel()
     {
         if (Input.GetKeyDown(KeyCode.G))
         {
             if (!isMenuopen)
             {
                 isMenuopen = true;
                 ItemMenu.SetActive(true);     
             }
             else if (isMenuopen)
             {
                 isMenuopen = false;
                 ItemMenu.SetActive(false);
             }
        
         }
     }*/
}

