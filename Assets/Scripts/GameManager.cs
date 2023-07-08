using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
    public static GameManager instance;
    public static bool gameManagerLoaded = false;

    public static float sensoryMetre;
    public static float socialBattery;

    public static string loadedScene;
    private const string SENSORY_METRE_KEY = "SensoryMetre";
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        //DontDestroyOnLoad(gameObject);
    }
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }
    public static void SaveSensoryMetre()
    {
        PlayerPrefs.SetFloat(SENSORY_METRE_KEY, sensoryMetre);
        PlayerPrefs.Save();
    }

    public static void LoadSensoryMetre()
    {
        if (PlayerPrefs.HasKey(SENSORY_METRE_KEY))
        {
            sensoryMetre = PlayerPrefs.GetFloat(SENSORY_METRE_KEY); //loads the value of the key
            Debug.Log("Loaded value of Sensory meter: " + GameManager.sensoryMetre);
        }
        else
        {
            sensoryMetre = 0f; //if there is no value, set it to 0
            Debug.Log("No value for Sensory Metre, set to 0");
        }
    }

    private static void OnDestroy()
    {
        SceneManager.UnloadSceneAsync("ManagementScene");
    }
}
