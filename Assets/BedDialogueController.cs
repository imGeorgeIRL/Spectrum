using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

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
        bool isDay = DialogueLua.GetVariable("isDay").asBool;
        bool bedTime = DialogueLua.GetVariable("BedTime").asBool;
        if (!isDay)
        {
            Debug.Log("It's night");
            if (bedTime)
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
