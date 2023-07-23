using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAtNight : MonoBehaviour
{
    public GameObject dialogueSystem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isDayTime)
        {
            if (GameManager.rhythmActive)
            {
                dialogueSystem.SetActive(false);
            }
            else
            {
                dialogueSystem.SetActive(true);
            }
        }
        else
        {
            dialogueSystem.SetActive(false);
        }
    }
}
