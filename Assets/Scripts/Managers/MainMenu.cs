using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        string lastLoadedScene = PlayerPrefs.GetString("lastLoadedScene", "Bedroom");

        SceneManager.LoadScene(lastLoadedScene);
        GameManager.loadedScene = "Bedroom";
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Application Quit");
        PlayerPrefs.DeleteAll();
        
    }

}
