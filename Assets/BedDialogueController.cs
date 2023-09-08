using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedDialogueController : MonoBehaviour
{
    public GameObject BedDialogueTrigger;

    // Start is called before the first frame update
    void Start()
    {
        BedDialogueTrigger.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isDayTime)
        {
            if (GameManager.goToSleep)
            {
                BedDialogueTrigger.SetActive(false);
            }
            else
            {
                BedDialogueTrigger.SetActive(true);
            }            
        }
        else
        {
            BedDialogueTrigger.SetActive(false);
        }
    }
}
