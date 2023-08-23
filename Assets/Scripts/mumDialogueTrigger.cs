using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mumDialogueTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    private bool inRangeOfMum;
    private bool phoneCall = false;

    public AudioSource audioSource;


    private void Awake()
    {
        inRangeOfMum = false;
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (inRangeOfMum && !DialogueManager.GetInstance().dialogueIsPlaying && !GameManager.talkedToMum)
        {
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
        audioSource.Play();
        yield return new WaitForSeconds(2f);
        audioSource.Stop();
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
