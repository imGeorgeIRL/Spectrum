using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightDialogue : MonoBehaviour
{
    public GameObject tvDialogueTrigger;

    // Start is called before the first frame update
    void Start()
    {
        tvDialogueTrigger.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        bool watchedTv = DialogueLua.GetVariable("interactedWithTv").asBool;
        bool watchingTv = DialogueLua.GetVariable("watchingTv").asBool;
        bool isDay = DialogueLua.GetVariable("isDay").asBool;

        if (!isDay)
        {
            if (watchingTv || watchedTv)
            {
                tvDialogueTrigger.SetActive(false);
            }
            else
            {
                tvDialogueTrigger.SetActive(true);
            }

        }
        else
        {
            tvDialogueTrigger.SetActive(false);
        }
    }
}
