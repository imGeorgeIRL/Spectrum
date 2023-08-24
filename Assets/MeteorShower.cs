using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MeteorShower : MonoBehaviour
{
    public GameObject meteor; // The object you want to spawn

    public Collider2D spawnArea;

    private float spawnInterval = 0.5f; // Time between spawns
    private float objectLifetime = 1.5f; // Time before objects are destroyed
    private Bounds spawnBounds; // Stores the bounds of the spawn area

    public Vector2 minScale = Vector2.one; // Minimum scale for the spawned objects
    public Vector2 maxScale = Vector2.one; // Maximum scale for the spawned objects

    private bool meteorStarted = false;

    //CAMERA STUFF
    public Camera cam;
    public float moveSpeed = 2.0f; // Speed of camera movement
    public float stopYPosition = 10.0f; // Y position where the camera should stop

    private bool isMoving = true;
    private bool coroutineStarted = false;

    public GameObject screenFade;
    private ScreenFader screenFaderScript;

    private void Start()
    {
        screenFaderScript = screenFade.GetComponent<ScreenFader>();
    }
    private void Update()
    {
        if (GameManager.meteorShower)
        {
            if (!meteorStarted)
            {
                if (spawnArea != null)
                {
                    meteorStarted = true;
                    // Calculate the bounds of the spawn area
                    spawnBounds = spawnArea.bounds;

                    // Start spawning objects repeatedly
                    InvokeRepeating("SpawnObject", 0f, spawnInterval);
                }
                else
                {
                    Debug.LogError("Spawn area collider not assigned!");
                }
            }

            if (isMoving)
            {
                // Move the camera upwards
                cam.transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);

                // Check if the camera has reached the stop point
                if (cam.transform.position.y >= stopYPosition && !coroutineStarted)
                {
                    isMoving = false;
                    StartCoroutine(EndGameNow());
                }
            }

        }
        // Start spawning objects repeatedly
        
    }

    private IEnumerator EndGameNow()
    {
        coroutineStarted = true;
        yield return new WaitForSeconds(3);
        GameManager.endOfGame = true;
        screenFaderScript.StartFade();
        yield return new WaitForSeconds(5);

        SceneManager.UnloadSceneAsync(GameManager.loadedScene);
        GameManager.loadedScene = "Credits";
        SceneManager.LoadSceneAsync("Credits", LoadSceneMode.Additive);
    }

    private void SpawnObject()
    {
        if (spawnArea == null)
        {
            return; // Return if the spawn area is not assigned
        }

        // Generate a random position within the bounds of the spawn area
        Vector2 randomPosition = new Vector2(
             Random.Range(spawnBounds.min.x, spawnBounds.max.x),
             Random.Range(spawnBounds.min.y, spawnBounds.max.y)
         );

        Vector2 randomScale = new Vector2(
            Random.Range(minScale.x, maxScale.x),
            Random.Range(minScale.y, maxScale.y)
        );



        // Instantiate the object at the random position
        GameObject spawnedObject = Instantiate(meteor, randomPosition, Quaternion.identity);
        spawnedObject.transform.localScale = new Vector3(randomScale.x, randomScale.y, 1f); // Set z-scale to 1 for 2D

        // Destroy the spawned object after a certain time
        Destroy(spawnedObject, objectLifetime);
    }
}
