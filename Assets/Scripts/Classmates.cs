using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classmates : MonoBehaviour
{
    public GameObject[] classmates;
    public GameObject lightsOff;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.dayOfWeek == 1)
        {
            foreach (GameObject fella in classmates)
            {
                fella.SetActive(false);
            }
            lightsOff.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
