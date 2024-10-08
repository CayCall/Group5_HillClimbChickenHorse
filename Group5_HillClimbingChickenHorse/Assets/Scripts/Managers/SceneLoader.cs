using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoaders : MonoBehaviour
{
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
}
