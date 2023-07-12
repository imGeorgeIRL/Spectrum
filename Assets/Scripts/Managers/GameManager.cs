using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
    public static GameManager instance;
    public static bool gameManagerLoaded = false;

    //Player stats *******************************************
    public static float sensoryMetre;
    public static float socialBattery = 100f;

    //Time of day ******************************************
    public static bool isDayTime = true;

    
    //Scene loading *****************************************
    private static string _loadedScene;
    public static string loadedScene 
    {
        get { return _loadedScene; }
        set
        {
            if (_loadedScene != value)
            {
                _loadedScene = value;
                if (instance != null)
                {
                    instance.StartCoroutine(instance.StartLoadingScreen());
                }
            }
        }
    }


    private const string SENSORY_METRE_KEY = "SensoryMetre";
    private const string SOCIAL_BATTERY_KEY = "SocialBattery";

    public GameObject loadingScreen;
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
    private void Start()
    {
        StartCoroutine(StartLoadingScreen());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(StartLoadingScreen());
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!isDayTime)
            {
                isDayTime = true;
            }
            else
            {
                isDayTime = false;
            }
            
        }
    }

    public IEnumerator StartLoadingScreen()
    {
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(2);
        loadingScreen.SetActive(false);
    }

    public static void SaveSensoryMetre()
    {
        PlayerPrefs.SetFloat(SENSORY_METRE_KEY, sensoryMetre);
        PlayerPrefs.Save();
    }

    public static void SaveSocialBattery()
    {
        PlayerPrefs.SetFloat(SOCIAL_BATTERY_KEY, socialBattery);
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

    public static void LoadSocialBattery()
    {
        if (PlayerPrefs.HasKey(SOCIAL_BATTERY_KEY))
        {
            socialBattery = PlayerPrefs.GetFloat(SOCIAL_BATTERY_KEY); //loads the value of the key
            Debug.Log("Loaded value of Social Battery: " + GameManager.socialBattery);
        }
        else
        {
            socialBattery = 100f; //if there is no value, set it to 100
            Debug.Log("No value for Social Battery, set to 100");
        }
    }




    private static void OnDestroy()
    {
        SceneManager.UnloadSceneAsync("ManagementScene");
    }
}
