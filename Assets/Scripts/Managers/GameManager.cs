using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
    public static GameManager instance;
    public static bool gameManagerLoaded = false;

    public static bool waitingForDialogue = false;
    public static bool menuActive = false;

    public static bool endOfGame = false;

    public static int preLoad = 0;

    //Player stats *******************************************
    public static float sensoryMetre;
    public static float socialBattery;
    public static bool rhythmActive;

    public static bool isSitting = false;
    public static bool isTalking;

    public static bool spokenToMiller;

    public static bool safeZoneActive = false;
    public static bool enteredCafe = false;

    public static bool isHavingMeltdown = false;
    public static bool talkingToNoah = false; 
    public static bool calmingDown = false;
    public static bool noahWalkAway = false;
    public static bool noahSitOnGround = false;

    public static bool whiteboardInactive = false;

    public static bool rhythmDeactivate = false;
    public static bool canMoveWhileMeltdown = false;
    //Time of day ******************************************
    public static bool isDayTime = true;
    public static bool goToSleep = false;

    public static int dayOfWeek = 0; //CHANGED FOR TESTING, INIT TO 0
    public static string sceneOfDay; 

    public static bool tuesdayMeltdown = false;
    //TEXTURES***********************************************
    public static bool goodTexture;
    public static bool badTexture;
    //Scene loading *****************************************
    public static bool leftUniTuesday = false;


    public static string firstSceneInSession;
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
                    PlayerPrefs.SetInt("IsDayTime", isDayTime ? 1 : 0);
                }
            }
        }
    }

    public static bool timeSkip;
    public static string timeSkipDestination;

    //Bus stuff***************************************************

    public static int choiceSelected;
    public static bool isbusChosen = false;


    private const string SENSORY_METRE_KEY = "SensoryMetre";
    private const string SOCIAL_BATTERY_KEY = "SocialBattery";

    public GameObject loadingScreen;

    //LIVING ROOM*************************************************
    public static bool watchingTv = false;
    public static int tvChoice;
    public static bool spaceDoc, news, realityTv = false;


    //OBJECT INTERACTIONS
    public static bool interactedWithWardrobe;

    public static bool talkedToMum = false;
    public static bool triggerDialogue = false;

    public static bool inRangeOfClothes = false;

    //PEOPLE and STUFF
    public static bool noahVisibleTuesday = false;
    public static bool noahVisibleWednesday = false;

    public static bool sitFaceForward = false;
    public static bool meteorShower = false;

    public static bool startPanic = false;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        StartCoroutine(StartLoadingScreen());
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
        
        LoadSensoryMetre();
        LoadSocialBattery();
        //isDayTime = true;
        //GameManager.isDayTime = PlayerPrefs.GetInt("IsDayTime") == 1;
        Debug.Log("Day time is " + isDayTime);

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

        
        //if (Input.GetKeyDown(KeyCode.N))
        //{
        //    sensoryMetre += 10f;

        //}

        //if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.P))
        //{
        //    PlayerPrefs.DeleteAll();
        //}



    }
   
    public IEnumerator StartLoadingScreen()
    {
        preLoad = 1;
        isTalking = false;
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
