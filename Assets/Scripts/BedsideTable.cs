using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class BedsideTable : MonoBehaviour
{
    public GameObject dialogueSystem;
    // Start is called before the first frame update
    void Start()
    {
        dialogueSystem.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        string dayOfWeek = DialogueLua.GetVariable("dayOfWeek").AsString;
        bool isDay = DialogueLua.GetVariable("isDay").asBool;
        if (dayOfWeek == "Tuesday" && !isDay)
        {
            dialogueSystem.SetActive(false);
        }
        
    }
}
