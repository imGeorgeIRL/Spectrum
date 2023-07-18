using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusSpawner : MonoBehaviour
{
    public GameObject busPrefab;
    public Transform targetBusPosition;

    private bool busSpawned;
    // Start is called before the first frame update
    void Start()
    {
        busSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.choiceSelected != 0 && GameManager.isbusChosen && !busSpawned)
        {
            SpawnBus();
            busSpawned = true;
            GameManager.isbusChosen = false;
        }
    }

    public void SpawnBus()
    {
        Instantiate(busPrefab, targetBusPosition.position, Quaternion.identity);
        GameManager.isbusChosen = true;
        busSpawned = false;
    }
}
