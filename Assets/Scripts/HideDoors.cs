using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideDoors : MonoBehaviour
{
    public GameObject[] doorTriggers;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.talkingToNoah)
        {
            foreach (GameObject door in doorTriggers) 
            {
                door.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject door in doorTriggers)
            {
                door.SetActive(true);
            }
        }
    }
}
