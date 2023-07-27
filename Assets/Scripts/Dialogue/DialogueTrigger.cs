using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    private bool playerInRange;

    private bool triggerDialogueInRange = false;
    private bool isCoolDown = false;

    private void Awake()
    {
        GameManager.isTalking = false;
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                
                TriggerDialogue();
                
            }
            if (playerInRange && triggerDialogueInRange)
            {
                if (!isCoolDown)
                {
                    isCoolDown = true;
                    TriggerDialogue();
                    StartCoroutine(CoolDown());
                }                    
            }            
        }
        else
        {
            visualCue.SetActive(false);
        }
    }
    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(15);
        isCoolDown = false;
    }
    private void TriggerDialogue()
    {
        GameManager.isTalking = true;
        Debug.Log("is talking is " + GameManager.isTalking.ToString());
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;

            if (gameObject.tag == "BusStop")
            {
                GameManager.isbusChosen = true;
            }
            if (gameObject.tag == "TriggeredDialogue")
            {
                triggerDialogueInRange = true;
            }
            
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;

            if (gameObject.tag == "BusStop")
            {
                GameManager.isbusChosen = false;
            }
            if (gameObject.tag == "TriggeredDialogue")
            {
                triggerDialogueInRange = false;
            }
            
        }
    }
}
