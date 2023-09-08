using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DoorController : MonoBehaviour
{
    private bool isPlayerInRange;
    public GameObject visualCue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        visualCue.SetActive(true);
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Door Input Detected");
            isPlayerInRange = true;
            visualCue.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        visualCue.SetActive(false);
        isPlayerInRange = false;
    }



    // Start is called before the first frame update
    void Start()
    {
        visualCue.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            GameManager.isTalking = false;
            switch (gameObject.tag)
            {
                case "BedroomDoor":
                    GameManager.transitionFromBedroom = true;
                    SceneManager.UnloadSceneAsync(GameManager.loadedScene);
                    GameManager.loadedScene = "Bedroom";                    
                    SceneManager.LoadSceneAsync("Bedroom", LoadSceneMode.Additive);
                    //StartCoroutine(WaitForLoad());


                    GameManager.SaveSensoryMetre();
                    GameManager.SaveSocialBattery();
                    
                    
                    break;

                case "LivingRoomDoor":
                    GameManager.transitionFromBedroom = true;
                    SceneManager.UnloadSceneAsync(GameManager.loadedScene);
                    GameManager.loadedScene = "LoungeKitchen";                    
                    SceneManager.LoadSceneAsync("LoungeKitchen", LoadSceneMode.Additive);
                    //StartCoroutine(WaitForLoad());
                    

                    GameManager.SaveSensoryMetre();
                    GameManager.SaveSocialBattery();
                    
                    break;

                case "GoInsideAustinsHouse": //Load the living room scene, but from the OUTSIDE, not coming from the bedroom.
                    GameManager.transitionFromBedroom = false;
                    SceneManager.UnloadSceneAsync(GameManager.loadedScene);
                    GameManager.loadedScene = "LoungeKitchen";
                    SceneManager.LoadSceneAsync("LoungeKitchen", LoadSceneMode.Additive);
                    //StartCoroutine(WaitForLoad());


                    GameManager.SaveSensoryMetre();
                    GameManager.SaveSocialBattery();

                    break;
                case "FrontDoor":
                    SceneManager.UnloadSceneAsync(GameManager.loadedScene);
                    GameManager.loadedScene = "Outside";                    
                    SceneManager.LoadSceneAsync("Outside", LoadSceneMode.Additive);
                    //StartCoroutine(WaitForLoad());
                    

                    GameManager.SaveSensoryMetre();
                    GameManager.SaveSocialBattery();

                    
                    break;
                case "UniFrontDoor":
                    SceneManager.UnloadSceneAsync(GameManager.loadedScene);
                    GameManager.loadedScene = "UniClassroom";
                    SceneManager.LoadSceneAsync("UniClassroom", LoadSceneMode.Additive);

                    GameManager.SaveSensoryMetre();
                    GameManager.SaveSocialBattery();
                    break;

                case "UniInteriorDoor":
                    SceneManager.UnloadSceneAsync(GameManager.loadedScene);
                    GameManager.loadedScene = "UniEntrance";
                    SceneManager.LoadSceneAsync("UniEntrance", LoadSceneMode.Additive);

                    GameManager.SaveSensoryMetre();
                    GameManager.SaveSocialBattery();

                    if (GameManager.dayOfWeek == 1)
                    {
                        GameManager.leftUniTuesday = true;
                    }

                    break;
                case "CafeInteriorDoor":
                    SceneManager.UnloadSceneAsync(GameManager.loadedScene);
                    GameManager.loadedScene = "TownCentre";
                    SceneManager.LoadSceneAsync("TownCentre", LoadSceneMode.Additive);

                    GameManager.SaveSensoryMetre();
                    GameManager.SaveSocialBattery();
                    break;
                case "CafeExteriorDoor":
                    SceneManager.UnloadSceneAsync(GameManager.loadedScene);
                    GameManager.loadedScene = "Cafe";
                    SceneManager.LoadSceneAsync("Cafe", LoadSceneMode.Additive);

                    GameManager.SaveSensoryMetre();
                    GameManager.SaveSocialBattery();
                    break;
                case "ThreadsCo":
                    SceneManager.UnloadSceneAsync(GameManager.loadedScene);
                    GameManager.loadedScene = "ThreadsCo";
                    SceneManager.LoadSceneAsync("ThreadsCo", LoadSceneMode.Additive);

                    GameManager.SaveSensoryMetre();
                    GameManager.SaveSocialBattery();
                    break;
                case "Observatory":
                    SceneManager.UnloadSceneAsync(GameManager.loadedScene);
                    GameManager.loadedScene = "Observatory";
                    SceneManager.LoadSceneAsync("Observatory", LoadSceneMode.Additive);

                    GameManager.SaveSensoryMetre();
                    GameManager.SaveSocialBattery();
                    break;
                    case "AfterObservatory":
                    SceneManager.UnloadSceneAsync(GameManager.loadedScene);
                    GameManager.loadedScene = "AfterObservatory";
                    SceneManager.LoadSceneAsync("AfterObservatory", LoadSceneMode.Additive);

                    GameManager.SaveSensoryMetre();
                    GameManager.SaveSocialBattery();
                    break;
                default:
                    Debug.LogWarning("No scene assigned for this door.");
                    break;


            }
        }
    }

    private IEnumerator WaitForLoad()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(GameManager.loadedScene));                
    }

}
