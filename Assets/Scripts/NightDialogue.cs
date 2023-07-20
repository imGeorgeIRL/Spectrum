using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightDialogue : MonoBehaviour
{
    public GameObject dialogueTrigger;
    // Start is called before the first frame update
    void Start()
    {
        dialogueTrigger.SetActive(false);
        Debug.Log("Dialogue is Inactive for " + name);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isDayTime)
        {
            dialogueTrigger.SetActive(true);
        }
        else
        {
            dialogueTrigger.SetActive(false);
        }
    }
}
