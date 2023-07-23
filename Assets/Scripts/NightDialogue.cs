using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightDialogue : MonoBehaviour
{
    public GameObject dialogueTrigger;
    // Start is called before the first frame update
    void Start()
    {
        dialogueTrigger.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isDayTime)
        {
            if (GameManager.watchingTv)
            {
                dialogueTrigger.SetActive(false);
            }
            else
            {
                dialogueTrigger.SetActive(true);
            }
        }
        else
        {
            dialogueTrigger.SetActive(false);
        }
    }
}
