using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusSpawner : MonoBehaviour
{
    public GameObject busPrefab;
    public Transform targetBusPosition;

    private bool busSpawned;
    private int busNumber;
    // Start is called before the first frame update
    void Start()
    {
        
        busNumber = DialogueLua.GetVariable("busChosen").asInt;
    }
    

    // Update is called once per frame
    void Update()
    {
        
        //if (busNumber != 0 && !busSpawned)
        //{
        //    SpawnBus();
        //    busSpawned = true;
            
        //}
    }

    public void SpawnBus()
    {
        Instantiate(busPrefab, targetBusPosition.position, Quaternion.identity);
        
        
    }
}
