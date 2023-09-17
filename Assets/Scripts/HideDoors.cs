using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideDoors : MonoBehaviour
{
    public GameObject[] doorTriggers;

    public GameObject threadsTrigger;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.dayOfWeek != 1)
        {
            foreach (GameObject door in doorTriggers)
            {
                door.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.dayOfWeek == 1)
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
        else if (GameManager.dayOfWeek == 2)
        {
            foreach (GameObject door in doorTriggers)
            {
                door.SetActive(false);
            }
        }
         //Threads
        if (GameManager.dayOfWeek == 2)
        {
            threadsTrigger.SetActive(true);
        }
        else
        {
            threadsTrigger.SetActive(false);
        }

        //Hiding other cafe's once youve gone into the right one

        if (GameManager.enteredCafe)
        {
            doorTriggers[0].SetActive(false);
            doorTriggers[1].SetActive(false);
            doorTriggers[3].SetActive(false);
        }
    }
}
