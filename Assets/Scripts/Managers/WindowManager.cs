using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (GameManager.isDayTime)
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
