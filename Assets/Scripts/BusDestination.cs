using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BusDestination : MonoBehaviour
{
    private float moveSpeed = 5f;
    //private string playerTag = "Player";
    private bool isMoving = true;
    
    private Rigidbody2D prefabRigidbody;

    private string sceneToLoad;

    private int busNumber;
    // Start is called before the first frame update
    void Start()
    {
        prefabRigidbody = GetComponent<Rigidbody2D>();
        busNumber = DialogueLua.GetVariable("busChosen").asInt;
    }
    private void OnDisable()
    {
        busNumber = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            // Calculate the direction towards the player
            Vector3 direction = GetPlayerPosition() - transform.position;
            direction.y = 0f; // Ignore vertical movement

            // Normalize the direction and apply the desired speed
            direction.Normalize();
            Vector3 velocity = direction * moveSpeed;

            // Apply the velocity to the Rigidbody
            prefabRigidbody.velocity = velocity;
        }
        else
        {
            prefabRigidbody.velocity = Vector3.zero;
        }
    }
    private Vector3 GetPlayerPosition()
    {
        // Find the player object using the specified tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            return player.transform.position;
        }

        // Return a default position if player object is not found
        return Vector3.zero;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter called");
        // Check if the prefab collided with the player object
        if (other.gameObject.tag == "Player")
        {
            // Stop the prefab from moving
            isMoving = false;
            Debug.Log("Choice selected: " + busNumber);
            if (GameManager.loadedScene == "Outside")
            {
                switch (busNumber)
                {
                    case 519:
                        //leads to town centre
                        sceneToLoad = "TownCentre";
                        StartCoroutine(waitForBus());
                        break;
                    case 838:
                        //leads to university
                        sceneToLoad = "UniEntrance";
                        StartCoroutine(waitForBus());
                        break;
                    case 827:
                        //leads to bus terminal
                        sceneToLoad = "BusTerminal";
                        StartCoroutine(waitForBus());
                        break;

                    default:
                        Debug.Log("there was no value for choiceSelected!");
                        break;
                }
            }
            else if (GameManager.loadedScene == "UniEntrance")
            {
                switch (GameManager.choiceSelected)
                {
                    case 1:
                        //Go home
                        sceneToLoad = "Outside";
                        StartCoroutine(waitForBus());
                        break;
                    case 2:
                        //Go to Town Centre
                        sceneToLoad = "TownCentre";
                        StartCoroutine(waitForBus());
                        break;
                    default:
                        Debug.Log("there was no value for choiceSelected!");
                        break;
                }
            }
            else if (GameManager.loadedScene == "BusTerminal")
            {
                switch (GameManager.choiceSelected)
                {
                    case 1:
                        //Go home
                        sceneToLoad = "UniEntrance";
                        StartCoroutine(waitForBus());
                        break;
                    default:
                        Debug.Log("there was no value for choiceSelected!");
                        break;
                }
            }
            else if (GameManager.loadedScene == "TownCentre")
            {
                switch (GameManager.choiceSelected)
                {
                    case 1:
                        //Go home
                        sceneToLoad = "Outside";
                        StartCoroutine(waitForBus());
                        break;
                    case 2:
                        //Go to Uni
                        sceneToLoad = "UniEntrance";
                        StartCoroutine(waitForBus());
                        break;
                    default:
                        Debug.Log("there was no value for choiceSelected!");
                        break;
                }
                
            }

        }
    }

    private IEnumerator waitForBus()
    {
        GameManager.choiceSelected = 0;
        yield return new WaitForSeconds(1.5f);
        
        SceneManager.UnloadSceneAsync(GameManager.loadedScene);
        GameManager.loadedScene = sceneToLoad;
        SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);

        GameManager.SaveSensoryMetre();
        GameManager.SaveSocialBattery();
        GameObject.Destroy(gameObject);
    }
}
