using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupManager : MonoBehaviour
{
    public string sceneToLoad;
    private void Start()
    {
        //string currentSceneName = SceneManager.GetActiveScene().name;
        //Debug.Log("Current scene is " + currentSceneName);
        if (!GameManager.gameManagerLoaded)
        {
            SceneManager.LoadSceneAsync("ManagementScene", LoadSceneMode.Additive);

            GameManager.loadedScene = sceneToLoad;

            GameManager.gameManagerLoaded = true;
            StartCoroutine(WaitForLoadedScene());
            
        }
        else
        {
            return;
        }
        // Load the game manager scene asynchronously

        GameManager.isTalking = false;
    }

    

    private IEnumerator WaitForLoadedScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneToLoad));
        string currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log("Current scene is " + currentSceneName);
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("Current scene is " + GameManager.loadedScene);
        }
    }
}
