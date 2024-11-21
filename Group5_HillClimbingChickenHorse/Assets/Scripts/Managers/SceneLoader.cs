using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoaders : MonoBehaviour
{
    public static SceneLoaders sceneInstance;
    private void Awake()
    {
        if ( sceneInstance == null)
        {
            sceneInstance= this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void LoadStartScene()
    {
        SceneManager.LoadScene("Scenes/StartScene");    
    }
    public void LoadMainScene()
    {
        SceneManager.LoadScene("Scenes/Micheal/Level 1");
    }


    public void OnApplicationQuit()
    {
        Application.Quit();
    }

    //Pause menus 
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1f;    
    }
    
}
