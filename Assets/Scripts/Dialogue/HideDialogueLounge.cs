using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideDialogueLounge : MonoBehaviour
{
    public GameObject[] dialogueTriggers;
    public GameObject falseDoor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.dayOfWeek == 2 && !GameManager.isDayTime)
        {
            foreach (GameObject dialogue in dialogueTriggers)
            {
                dialogue.SetActive(false);
            }
            falseDoor.SetActive(true);
        }
        else
        {
            foreach (GameObject dialogue in dialogueTriggers)
            {
                dialogue.SetActive(true);
            }
            falseDoor.SetActive(false);
        }
    }
}
