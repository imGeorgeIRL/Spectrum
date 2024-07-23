using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class WindowManager : MonoBehaviour
{

    public GameObject windowDay;
    public GameObject windowNight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool isDay = DialogueLua.GetVariable("isDay").asBool;
        if (isDay)
        {
            windowDay.SetActive(true);
            windowNight.SetActive(false);
        }
        else
        {
            windowDay.SetActive(false);
            windowNight.SetActive(true);
        }
    }
}
