using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoahHide : MonoBehaviour
{
    public GameObject wednesday;
    private bool noahVisible;
    private string dayOfWeek;

    // Start is called before the first frame update
    void Start()
    {
        noahVisible = DialogueLua.GetVariable("noahVisible").asBool;
        dayOfWeek = DialogueLua.GetVariable("dayOfWeek").asString;
    }

    // Update is called once per frame
    void Update()
    {
        //if ((GameManager.dayOfWeek == 1 && GameManager.noahVisibleTuesday) || (GameManager.noahVisibleWednesday && GameManager.dayOfWeek == 2))
        //{
        //    wednesday.SetActive(true);
            
        //}
        //else
        //{
        //    wednesday.SetActive(false);
            
        //}
        if (noahVisible)
        {
            wednesday.SetActive(true);
        }
        else
        {
            wednesday.SetActive(false);
        }

        
    }
}
