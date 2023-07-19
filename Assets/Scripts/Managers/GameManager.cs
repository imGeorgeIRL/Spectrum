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
    public static float socialBattery;

    public static bool isSitting = false;

    public static bool isTalking;

    //Time of day ******************************************
    public static bool isDayTime;
    public static bool goToSleep = false;

    //Scene loading *****************************************
    public static bool transitionFromBedroom;
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

    public static bool timeSkip;
    public static string timeSkipDestination;
    //Task Manager **********************************************

    public static bool eatenBreakfast, goneToUni, madeAFriend;



    //Bus stuff***************************************************

    public static int choiceSelected;
    public static bool isbusChosen = false;


    private const string SENSORY_METRE_KEY = "SensoryMetre";
    private const string SOCIAL_BATTERY_KEY = "SocialBattery";

    public GameObject loadingScreen;

    //LIVING ROOM*************************************************
    public static bool watchingTv = false;



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
        LoadSensoryMetre();
        LoadSocialBattery();
        isDayTime = true;
    }

    private void Update()
    {

        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    if (!isDayTime)
        //    {
        //        isDayTime = true;
        //    }
        //    else
        //    {
        //        isDayTime = false;
        //    }

        //}

        if (Input.GetKeyDown(KeyCode.N))
        {
            sensoryMetre += 10f;
            socialBattery -= 10f;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.DeleteAll();
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
            Debug.Log("Loaded value of Sensory meter: " + sensoryMetre);
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
            Debug.Log("Loaded value of Social Battery: " + socialBattery);
        }
        else
        {
            socialBattery = 100f; //if there is no value, set it to 100
            Debug.Log("No value for Social Battery, set to 100");
        }
    }
     



    private static void OnDestroy()
    {
        gameManagerLoaded = false;
        SceneManager.UnloadSceneAsync("ManagementScene");
        SaveSensoryMetre();
        SaveSocialBattery();
        
    }
}
