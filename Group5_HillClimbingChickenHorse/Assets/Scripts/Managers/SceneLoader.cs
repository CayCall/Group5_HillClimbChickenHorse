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
        SceneManager.LoadScene("");    
    }
    public void LoadMainScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void LoadEndScene()
    {
        SceneManager.LoadScene("");
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
