using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset triggerInkJSON;

    public float handMoveSpeed = 3f;
    private Animator anim;

    private bool good;
    private bool bad;
    private void Start()
    {
        anim = GetComponent<Animator>();
        good = false;
        bad = false;
        
    }
    private void Update()
    {
        if (!GameManager.isTalking)
        {
            // Hand movement logic
            Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
            transform.Translate(moveDirection * handMoveSpeed * Time.deltaTime);
        }
        else
        {
            handMoveSpeed = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!GameManager.isTalking)
            {
                anim.SetBool("Grab", true);
                if (good)
                {
                    //do fun stuff with visuals
                    Debug.LogWarning("good");
                    GameManager.goodTexture = true;
                    TriggerDialogue();
                }
                else if (bad)
                {
                    //do not so fun stuff with visuals or screen shake??
                    Debug.LogWarning("bad");
                    GameManager.badTexture = true;
                    TriggerDialogue();
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("Grab", false);
        }


        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GoodTexture"))
        {
            Debug.Log("This is a good texture :)");
            good = true;
        }
        if (other.CompareTag("BadTexture"))
        {
            Debug.Log("YEEEEEEEOWCH bad texture :C");
            bad = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("GoodTexture"))
        {
            good = false;
        }
        if (other.CompareTag("BadTexture"))
        {
            bad = false;
        }
    }

    public void TriggerDialogue()
    {
        GameManager.isTalking = true;
        Debug.Log("is talking is " + GameManager.isTalking.ToString());
        DialogueManager.GetInstance().EnterDialogueMode(triggerInkJSON);
    }
}
