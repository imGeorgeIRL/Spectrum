using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoahDialogueTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    private bool noahInRange;
    private bool phoneCall = false;

    public AudioSource audioSource;


    private void Awake()
    {
        noahInRange = false;
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (GameManager.dayOfWeek == 2)
        {
            if (noahInRange && !DialogueManager.GetInstance().dialogueIsPlaying && !GameManager.isDayTime)
            {
                if (!phoneCall)
                {
                    StartCoroutine(PhoneCall());
                    phoneCall = true;
                }
            }
        }
    }


    private IEnumerator PhoneCall()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("Phone call with noah active");
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
            noahInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            noahInRange = false;
        }
    }
}
