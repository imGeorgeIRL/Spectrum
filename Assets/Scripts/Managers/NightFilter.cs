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
        if (GameManager.isDayTime)
        {
            nightFilter.SetActive(false);
        }
        else
        {
            nightFilter.SetActive(true);
        }
    }
}
