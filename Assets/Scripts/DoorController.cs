using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DoorController : MonoBehaviour
{
    private bool isPlayerInRange;
    public GameObject visualCue;

    public string bedroomSceneName = "Bedroom";
    public string LoungeSceneName = "LoungeKitchen";


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

            switch (gameObject.tag)
            {
                case "BedroomDoor":
                    SceneManager.UnloadSceneAsync(GameManager.loadedScene);
                    GameManager.loadedScene = "Bedroom";

                    StartCoroutine(WaitForLoad());
                    
                    SceneManager.LoadSceneAsync("Bedroom", LoadSceneMode.Additive);
                    


                    GameManager.SaveSensoryMetre();
                    GameManager.SaveSocialBattery();
                    break;

                case "LivingRoomDoor":
                    SceneManager.UnloadSceneAsync(GameManager.loadedScene);
                    GameManager.loadedScene = "LoungeKitchen";
                    StartCoroutine(WaitForLoad());
                    SceneManager.LoadSceneAsync("LoungeKitchen", LoadSceneMode.Additive);
                    


                    GameManager.SaveSensoryMetre();
                    GameManager.SaveSocialBattery();
                    break;

                case "FrontDoor":
                    SceneManager.UnloadSceneAsync(GameManager.loadedScene);
                    GameManager.loadedScene = "Outside";
                    StartCoroutine(WaitForLoad());
                    SceneManager.LoadSceneAsync("Outside", LoadSceneMode.Additive);
                    


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
        yield return new WaitForSeconds(0.2f);
    }

}
