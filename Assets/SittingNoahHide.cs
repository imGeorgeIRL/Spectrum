using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SittingNoahHide : MonoBehaviour
{
    public GameObject noah;
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
        }
        else
        {
            noah.SetActive(true);
        }
    }
}
