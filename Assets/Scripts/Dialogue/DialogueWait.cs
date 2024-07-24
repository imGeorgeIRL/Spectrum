using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueWait : MonoBehaviour
{
    //this script is for when you need to wait a certain length of time before the dialogue is triggered

    public GameObject dialogue;
    public int seconds;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartDialogue());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartDialogue()
    {
        yield return new WaitForSeconds(seconds);
        dialogue.SetActive(true);
    }
}
