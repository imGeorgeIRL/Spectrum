using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderTrigger : MonoBehaviour
{

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    private bool playerInRange;
    private bool isCoolDown = false;


    private void Awake()
    {
        playerInRange = false;
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            if (!isCoolDown)
            {
                isCoolDown = true;
                
                TriggerDialogue();
                StartCoroutine(CoolDown());
            }
            
        }
    }

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(15);
        isCoolDown = false;
    }

    private void TriggerDialogue()
    {
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
