using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorShower : MonoBehaviour
{
    public GameObject meteor; // The object you want to spawn
    public Vector3 spawnAreaCenter; // Center of the spawn area
    public Vector3 spawnAreaSize; // Size of the spawn area
    private float spawnInterval = 2f; // Time between spawns
    private float objectLifetime = 2f; // Time before objects are destroyed

    private void Start()
    {
        // Start spawning objects repeatedly
        InvokeRepeating("SpawnObject", 0f, spawnInterval);
    }

    private void SpawnObject()
    {
        // Generate a random position within the spawn area
        Vector3 randomPosition = new Vector3(
            Random.Range(spawnAreaCenter.x - spawnAreaSize.x / 2, spawnAreaCenter.x + spawnAreaSize.x / 2),
            Random.Range(spawnAreaCenter.y - spawnAreaSize.y / 2, spawnAreaCenter.y + spawnAreaSize.y / 2),
            Random.Range(spawnAreaCenter.z - spawnAreaSize.z / 2, spawnAreaCenter.z + spawnAreaSize.z / 2)
        );

        // Instantiate the object at the random position
        GameObject spawnedObject = Instantiate(meteor, randomPosition, Quaternion.identity);

        // Destroy the spawned object after a certain time
        Destroy(spawnedObject, objectLifetime);
    }
}
