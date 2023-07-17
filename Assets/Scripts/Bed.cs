using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    public GameObject dialogueTrigger;
    // Start is called before the first frame update
    void Start()
    {
        dialogueTrigger.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isDayTime)
        {
            dialogueTrigger.SetActive(true);
        }
    }
}
