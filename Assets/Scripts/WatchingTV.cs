using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        animator = character.GetComponent<Animator>();
        watching = false;
        tvScreen.SetActive(false);
        couchRenderer = GetComponent<Renderer>();
        bx = GetComponent<BoxCollider2D>();
        bx.enabled = false;
        sitPosition = new Vector3(-1.7f, 0.52f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.watchingTv && !watching)
        {            
            StartCoroutine(WatchTV());            
        }

        if (!GameManager.watchingTv && watching)
        {
            watching = false;
            bx.enabled = false;
            //StartCoroutine(StopWatchingTV());
            character.transform.position = startingPosition;
            character.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            animator.SetBool("isSitting", false);
            tvScreen.SetActive(false);
            couchRenderer.sortingOrder = 3;
        }
    }

    private IEnumerator WatchTV()
    {
        watching = true;
        startingPosition = character.transform.position;
        yield return new WaitForSeconds(3f);
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
