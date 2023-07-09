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
                    SceneManager.LoadSceneAsync(bedroomSceneName, LoadSceneMode.Additive);
                    GameManager.loadedScene = bedroomSceneName;
                    GameManager.SaveSensoryMetre();
                    break;

                case "LivingRoomDoor":
                    SceneManager.UnloadSceneAsync(GameManager.loadedScene);
                    SceneManager.LoadSceneAsync(LoungeSceneName, LoadSceneMode.Additive);
                    GameManager.loadedScene = LoungeSceneName;
                    GameManager.SaveSensoryMetre();
                    break;

                default:
                    Debug.LogWarning("No scene assigned for this door.");
                    break;


            }
        }
    }

}
