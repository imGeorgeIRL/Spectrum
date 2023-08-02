using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeSkip : MonoBehaviour
{
    private bool hasSkipped;
    // Start is called before the first frame update
    void Start()
    {
        hasSkipped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.timeSkip && !hasSkipped)
        {
            switch (GameManager.loadedScene)
            {
                case "UniClassroom":
                    hasSkipped = true;
                    GameManager.timeSkipDestination = "UniEntrance";
                    StartCoroutine(WaitForTimeSkip());
                    break;
                case "TownCentre":
                    hasSkipped = true;
                    GameManager.timeSkipDestination = "LoungeKitchen";
                    StartCoroutine(WaitForTimeSkip());
                    GameManager.isDayTime = false;
                    break;

                default:
                    hasSkipped = true;
                    Debug.LogWarning("No timeskip destination found");
                    break;
            }
        }
    }


    private IEnumerator WaitForTimeSkip()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.UnloadSceneAsync(GameManager.loadedScene);
        SceneManager.LoadSceneAsync(GameManager.timeSkipDestination, LoadSceneMode.Additive);
        GameManager.loadedScene = GameManager.timeSkipDestination;

        GameManager.SaveSensoryMetre();
        GameManager.SaveSocialBattery();
        GameManager.timeSkip = false;  
    }
}
