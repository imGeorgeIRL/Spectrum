using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SittingNoahHide : MonoBehaviour
{
    public GameObject noah;

    public GameObject[] panicDialogues;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.dayOfWeek != 0)
        {
            noah.SetActive(false);
            foreach (GameObject go in panicDialogues)
            {
                go.SetActive(true);
            }
        }
        else
        {
            noah.SetActive(true);
            foreach (GameObject go in panicDialogues)
            {
                go.SetActive(false);
            }
        }
    }
}
