using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStop : MonoBehaviour
{
    public GameObject austin, noah;

    private Animator animAust, animNoh;
    private PlayerMovement playerMove;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset triggerInkJSON;

    private bool coroutinePlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        animAust = austin.GetComponent<Animator>();
        animNoh = noah.GetComponent<Animator>();

        playerMove = austin.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !coroutinePlaying)
        {
            playerMove.enabled = false;
            StartCoroutine(EndGameDialogue());
        }
    }
    
    private IEnumerator EndGameDialogue()
    {
        coroutinePlaying = true;
        yield return new WaitForSeconds(2);
        animAust.SetTrigger("StandForward");
        
        yield return new WaitForSeconds(2);
        TriggerDialogue();
        yield return new WaitForSeconds(2);
        animNoh.SetTrigger("StandForward");
        Vector3 currentPosition = noah.transform.position;
        currentPosition.y -= 0.5f;
        noah.transform.position = currentPosition;
    }
    private void TriggerDialogue()
    {
        GameManager.isTalking = true;
        DialogueManager.GetInstance().EnterDialogueMode(triggerInkJSON);
    }


    /* What to do?
     - when austin reaches the trigger
        - stop for a few seconds
        - Make them stand forwards
        - stop movement
        - trigger dialogue
    - halfway through conversation
        - sit down
        - meteor shower starts
        - conversation stops
        - UI panels disappear
        - Camera pans up
        - boom. Title screen
        - wait a few seconds
        - trigger credits.
     
     
     */
}
