using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadStats : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.LoadSensoryMetre();
        GameManager.LoadSocialBattery();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
