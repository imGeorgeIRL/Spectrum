using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("ContentWarning");        
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Application Quit");        
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void ClearSave()
    {
        PlayerPrefs.DeleteAll();
        GameManager.dayOfWeek = 0;
        SceneManager.LoadScene("ContentWarning");
    }
}
