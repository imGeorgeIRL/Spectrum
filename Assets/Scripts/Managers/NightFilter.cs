using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightFilter : MonoBehaviour
{
    public GameObject nightFilter;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool isDay = DialogueLua.GetVariable("isDay").asBool;
        if (isDay)
        {            
            nightFilter.SetActive(false);          
        }
        else
        {
            if (GameManager.loadedScene == "Bedroom" || GameManager.loadedScene == "LoungeKitchen" ||
                GameManager.loadedScene == "BusTerminal" || GameManager.loadedScene == "UniClassroom")
            {
                nightFilter.SetActive(false);
            }
            else
            {
                nightFilter.SetActive(true);
            }
         
        }
    }
}
