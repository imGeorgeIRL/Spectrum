using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    private bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                isPaused = true;
                Time.timeScale = 0f;
                pauseMenu.SetActive(true); 
                GameManager.menuActive = true;
            }
            else
            {
                isPaused = false;
                Time.timeScale = 1f;
                pauseMenu.SetActive(false);
                GameManager.menuActive = false;
            }
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void MainMenuButton()
    {
        PlayerPrefs.SetString("lastLoadedScene", GameManager.loadedScene);
        Time.timeScale = 1f;
        GameManager.gameManagerLoaded = false;
        SceneManager.UnloadSceneAsync("ManagementScene");
        GameManager.loadedScene = "MainMenu";
        SceneManager.LoadScene("MainMenu");
        PlayerPrefs.Save();
    }
}
