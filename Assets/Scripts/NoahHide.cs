using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoahHide : MonoBehaviour
{
    public GameObject wednesday;
    public GameObject noahTrigger;
    private DialogueTrigger triggerScript;
    // Start is called before the first frame update
    void Start()
    {
        triggerScript = noahTrigger.GetComponent<DialogueTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.dayOfWeek == 2 && GameManager.noahVisibleWednesday)
        {
            wednesday.SetActive(true);
            triggerScript.enabled = true;
        }
        else
        {
            wednesday.SetActive(false);
            triggerScript.enabled = false;
        }
    }
}
