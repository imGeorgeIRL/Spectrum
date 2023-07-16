using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public GameObject tick1, tick2, tick3;

    public GameObject planner;

    private bool plannerActive;
    // Start is called before the first frame update
    void Start()
    {
        tick1.SetActive(false);
        tick2.SetActive(false);
        tick3.SetActive(false);
        planner.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            planner.SetActive(true);
            plannerActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && plannerActive)
        {
            planner.SetActive(false);
            plannerActive = false;
        }


            if (GameManager.eatenBreakfast)
        {
            tick1.SetActive(true);
        }
        else if (GameManager.goneToUni)
        {
            tick2.SetActive(true);
        }
        else if (GameManager.madeAFriend) 
        {
            tick3.SetActive(true);
        }
    }
}
