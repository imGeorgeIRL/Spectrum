using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideWIndowManager : MonoBehaviour
{
    public GameObject[] windowArray;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isDayTime)
        {
            foreach (GameObject window in windowArray)
            {
                window.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject window in windowArray)
            {
                window.SetActive(true);
            }
        }
    }
}
