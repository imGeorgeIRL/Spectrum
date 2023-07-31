using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedsideTable : MonoBehaviour
{
    public GameObject dialogueSystem;
    // Start is called before the first frame update
    void Start()
    {
        dialogueSystem.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.interactedWithWardrobe && !GameManager.rhythmActive)
        {
            dialogueSystem.SetActive(true);
        }
        else
        {
            dialogueSystem.SetActive(false);
        }
    }
}
