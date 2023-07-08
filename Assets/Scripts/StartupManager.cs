using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupManager : MonoBehaviour
{
    private void Start()
    {
        if (!GameManager.gameManagerLoaded)
        {
            SceneManager.LoadSceneAsync("ManagementScene", LoadSceneMode.Additive);
            GameManager.loadedScene = "Bedroom";
            GameManager.gameManagerLoaded = true;
        }
        else
        {
            return;
        }
        // Load the game manager scene asynchronously

    }
}
