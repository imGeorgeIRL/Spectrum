using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoahMovement : MonoBehaviour
{
    [SerializeField] private GameObject austin;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject doorDialogue;
    [SerializeField] private Transform leavingTransform;
    private BoxCollider2D wallCollider;
    private float moveSpeed = 4f;
    private float stoppingDistance = 3f;

    private Rigidbody2D rb;
    private Animator anim;

    private bool isMoving = false;

    private bool noahCanMove = true;

    private RhythmGame rhythmGame;
    

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        anim = GetComponent<Animator>();

        wallCollider = wall.GetComponentInChildren<BoxCollider2D>();
        rhythmGame = austin.GetComponent<RhythmGame>();

        if (GameManager.dayOfWeek == 1)
        {
            doorDialogue.SetActive(false);
        }
    }

    private IEnumerator WaitForAustin()
    {
        yield return new WaitForSeconds(8);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isHavingMeltdown && noahCanMove && GameManager.loadedScene == "UniClassroom")
        {
            MoveTowardsAustin();
            GameManager.whiteboardInactive = true;
        }

        if (isMoving)
        {            
            wallCollider.enabled = false;
        }
        else
        {
            anim.Play("Noah_Idle_Right");
            wallCollider.enabled = true;
        }

        if (GameManager.noahWalkAway)
        {
            MoveAwayFromAustin();
            GameManager.sceneOfDay = "cafeTuesday";
            doorDialogue.SetActive(true);
        }
        if (GameManager.calmingDown)
        {
            rhythmGame.enabled = true;
        }
        if (GameManager.dayOfWeek == 1 && !GameManager.calmingDown && GameManager.loadedScene == "UniClassroom")
        {
            rhythmGame.enabled = false;
        }
    }

    private void MoveTowardsAustin()
    {
        Vector2 direction = austin.transform.position - transform.position;

        direction.y = 0;

        float distance = direction.magnitude;

        if (distance > stoppingDistance)
        {
            anim.Play("Noah_Walk_Right");
            Vector2 movement = direction.normalized * moveSpeed * Time.deltaTime;
            movement.y = 0;

            transform.position += (Vector3)movement;
            isMoving = true;
        }
        if (distance <= stoppingDistance)
        {
            isMoving = false;
            noahCanMove = false;
        }

    }

    private void MoveAwayFromAustin()
    {
        Vector2 direction = leavingTransform.position - transform.position;

        direction.y = 0f;

        float distance = direction.magnitude;

        if (distance > stoppingDistance)
        {
            anim.Play("Noah_Walk_Left");
            Vector2 movement = direction.normalized * moveSpeed * Time.deltaTime;
            movement.y = 0f;

            transform.position += (Vector3)movement;
            isMoving = true;
        }
        if (distance <= stoppingDistance)
        {
            isMoving = false;
            noahCanMove = false;
            GameManager.noahWalkAway = false;
            GameManager.talkingToNoah = true;
        }
    }
}
