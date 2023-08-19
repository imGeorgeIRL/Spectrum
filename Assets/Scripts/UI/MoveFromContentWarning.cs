using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveFromContentWarning : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(LoadNextScene());
    }

    private void ContentWarning()
    {
        string lastLoadedScene = PlayerPrefs.GetString("lastLoadedScene", "Bedroom");

        if (PlayerPrefs.HasKey("lastLoadedScene"))
        {
            SceneManager.LoadScene(lastLoadedScene);
            GameManager.loadedScene = lastLoadedScene;
        }
        else
        {
            Debug.Log("Last loaded scene was not found. Loading game from beginning");
            SceneManager.LoadScene("Bedroom");
        }
    }

    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(10);
        ContentWarning();
    }
}
