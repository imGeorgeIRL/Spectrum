using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{

    public GameObject[] npcArray;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isDayTime)
        {
            foreach (GameObject npc in npcArray)
            {
                npc.SetActive(true);
            }

        }
        else
        {
            foreach (GameObject npc in npcArray)
            {
                npc.SetActive(false);
            }

        }
    }
}
