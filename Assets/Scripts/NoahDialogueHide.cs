using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoahDialogueHide : MonoBehaviour
{
    public GameObject noahDialogue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.dayOfWeek == 0)
        {
            if (!GameManager.spokenToMiller)
            {
                noahDialogue.SetActive(false);
            }
            else
            {
                noahDialogue.SetActive(true);
            }
        }
        else
        {
            noahDialogue.SetActive(true);
        }
    }
}
