using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mumDialogueVisible : MonoBehaviour
{
    public GameObject dialogueTriggerMum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.dayOfWeek == 0)
        {
            dialogueTriggerMum.SetActive(false);
        }
        else
        {
            if (GameManager.talkedToMum)
            {
                dialogueTriggerMum.SetActive(false);
            }
            else
            {
                dialogueTriggerMum.SetActive(true);
            }

        }
    }
}
