using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoahHide : MonoBehaviour
{
    public GameObject wednesday;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.dayOfWeek == 2 && GameManager.noahVisibleWednesday)
        {
            wednesday.SetActive(true);
        }
        else
        {
            wednesday.SetActive(false);
        }
    }
}
