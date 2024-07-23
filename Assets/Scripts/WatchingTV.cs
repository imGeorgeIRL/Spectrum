using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class WatchingTV : MonoBehaviour
{
    private bool watching;
    public GameObject character;
    private Animator animator;
    public GameObject tvScreen;


    private BoxCollider2D bx;
    private Renderer couchRenderer;

    private Vector3 startingPosition;
    private Vector3 sitPosition;

    public AudioClip[] tvClips;
    private AudioSource audioSource;
    private int clipID;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = character.GetComponent<Animator>();
        watching = false;
        tvScreen.SetActive(false);
        couchRenderer = GetComponent<Renderer>();
        bx = GetComponent<BoxCollider2D>();
        bx.enabled = false;
        sitPosition = new Vector3(-1.7f, 0.52f, 0f);
    }

    public void WatchTv()
    {
        string watchingWhat = DialogueLua.GetVariable("watchingWhat").AsString;
        bool watchingTv = DialogueLua.GetVariable("watchingTv").AsBool;

        if (watchingTv && !watching)
        {
            if (watchingWhat == "spaceDoc")
            {
                clipID = 0;
            }
            else if (watchingWhat == "news")
            {
                clipID = 1;
            }
            else if (watchingWhat == "realityTv")
            {
                clipID = 2;
            }
            StartCoroutine(WatchTV());
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool watchingTv = DialogueLua.GetVariable("watchingTv").AsBool;
        string watchingWhat = DialogueLua.GetVariable("watchingWhat").AsString;

        if (watchingTv && !watching)
        {
            if (watchingWhat == "spaceDoc")
            {
                clipID = 0;
            }
            else if (watchingWhat == "news")
            {
                clipID = 1;
            }
            else if (watchingWhat == "realityTv")
            {
                clipID = 2;
            }
            StartCoroutine(WatchTV());
        }
        if (!watchingTv && watching)
        {
            watching = false;
            bx.enabled = false;

            //StartCoroutine(StopWatchingTV());
            character.transform.position = startingPosition;
            character.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);

            animator.SetBool("isSitting", false);
            tvScreen.SetActive(false);
            couchRenderer.sortingOrder = 3;

         

            if (watchingWhat == "spaceDoc")
            {
                GameManager.socialBattery += 15f;
                GameManager.sensoryMetre -= 8f;
            }
            else if (watchingWhat == "news")
            {
                GameManager.sensoryMetre += 5f;
                GameManager.socialBattery -= 10f;
            }
            else if (watchingWhat == "realityTv")
            {
                GameManager.sensoryMetre += 15f;
            }
            audioSource.Stop();

        }
    }

    private IEnumerator WatchTV()
    {        
        watching = true;
        startingPosition = character.transform.position;

        yield return new WaitForSeconds(1f);

        AudioClip clip = tvClips[clipID];
        audioSource.clip = clip;
        audioSource.Play();

        bx.enabled = true;
        character.transform.position = sitPosition;
        character.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
        animator.SetBool("isSitting", true);
        tvScreen.SetActive(true);
        couchRenderer.sortingOrder = 11;
        
    }

    private IEnumerator StopWatchingTV()
    {
        watching = false;
        yield return new WaitForSeconds(2f);
        character.transform.position = startingPosition;
        character.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        animator.SetBool("isSitting", false);
        tvScreen.SetActive(false);
        couchRenderer.sortingOrder = 3;

    }
}
