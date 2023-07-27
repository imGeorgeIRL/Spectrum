using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mumDialogueTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    private bool inRangeOfMum;

    


    private void Awake()
    {
        inRangeOfMum = false;
    }

    private void Update()
    {
        if (inRangeOfMum && !DialogueManager.GetInstance().dialogueIsPlaying && !GameManager.talkedToMum)
        {
            bool phoneCall = false;
            if (!phoneCall)
            {
                StartCoroutine(PhoneCall());
                phoneCall = true;
            }
            
        }

    }


    private IEnumerator PhoneCall()
    {
        Debug.Log("Phone call with mum active");
        //phone rings - play sound
        yield return new WaitForSeconds(1f);
        TriggerDialogue();
    }


    private void TriggerDialogue()
    {
        GameManager.isTalking = true;
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inRangeOfMum = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inRangeOfMum = false;
        }
    }
}
