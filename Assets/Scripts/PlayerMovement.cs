using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private DoorController doorController;
    public float moveSpeed = 5f;

    private Animator animator;
    private Rigidbody2D rb;

    private string currentSceneName;
    private float savedXCoordinate;
    private Vector3 startingPosition;

    //public Animation leftIdle;
    //public Animation rightIdle;


    public Transform sitTransform;
    private Vector3 positionBeforeSit;
    private bool hasSat;

    private bool isLyingDown = false;
    
    // ******************************************************************************************
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
    }



    private void Start()
    {
        currentSceneName = GameManager.loadedScene;
        savedXCoordinate = LoadSavedXCoordinate();
        SaveXCoordinate(transform.position.x);
        ReenterRoom();
        
    }

    //private void OnEnable()
    //{
    //    ReenterRoom();
    //}

    private IEnumerator WaitToLieDown()
    {
        isLyingDown = true;
        yield return new WaitForSeconds(3);
        animator.SetBool("Meltdown", true);
        rb.velocity = Vector2.zero;
    }

    private void Update()
    {
        if (GameManager.dayOfWeek == 1 && GameManager.safeZoneActive && GameManager.tuesdayMeltdown)
        {
            if (!isLyingDown) 
            {
                StartCoroutine(WaitToLieDown());
            }

        }

        if (GameManager.sensoryMetre >= 85f && !GameManager.canMoveWhileMeltdown)
        {
            animator.SetFloat("Direction", 0f);
            animator.SetBool("isWalking", false);
            animator.SetBool("isPanic", true);
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            animator.speed = 3f;
        }
        else
        {
            if (DialogueManager.GetInstance().dialogueIsPlaying)
            {
                animator.SetBool("isWalking", false);

                //return;
            }
            else
            {
                animator.SetBool("isPanic", false);
                animator.speed = 1f;

                float moveHorizontal = Input.GetAxis("Horizontal");

                // Move the character horizontally
                Vector2 movement = new Vector2(moveHorizontal * moveSpeed, rb.velocity.y);
                rb.velocity = movement;


                if (moveHorizontal != 0f)
                {
                    bool isTalking = animator.GetBool("isTalking");
                    if (GameManager.isTalking)
                    {
                        if (moveHorizontal > 0f)
                        {
                            animator.Play("ANIM_Austin_Idle_Right");
                            animator.SetBool("isTalking", true);
                        }
                        else
                        {
                            animator.Play("ANIM_Austin_Idle_Left");
                            animator.SetBool("isTalking", true);
                        }
                    }
                    else
                    {
                        animator.SetBool("isWalking", true);
                        animator.SetFloat("Direction", moveHorizontal);
                        animator.SetBool("isTalking", false);

                        if (GameManager.watchingTv)
                        {
                            GameManager.watchingTv = false;
                        }
                    }
                }
                else
                {
                    animator.SetBool("isWalking", false);

                }

            }

            if (GameManager.isTalking && !hasSat)
            {
                animator.SetBool("isTalking", true);
                rb.velocity = Vector2.zero;
            }
            else
            {
                animator.SetBool("isTalking", false);
            }
            //*****************************************************

            if (GameManager.isSitting && GameManager.loadedScene == "UniClassroom" && !hasSat)
            {
                positionBeforeSit = transform.position;
                Debug.Log("Position saved at " + positionBeforeSit);
                SitDown();
                hasSat = true;
            }
            else if (GameManager.isSitting && GameManager.loadedScene == "Cafe" && !hasSat)
            {
                positionBeforeSit = transform.position;
                Debug.Log("Position saved at " + positionBeforeSit);
                SitDown();
                hasSat = true;
            }

            //******************************************************

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                if (hasSat)
                {
                    GameManager.isSitting = false;
                    StandUp();
                }
            }
        }
    }

    private void StandUp()
    {
        hasSat = false;
        isLyingDown = false;
        transform.position = new Vector3(-4.68f, 0.94f, 0f);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        animator.SetBool("isSitting", false);
        animator.SetBool("Meltdown", false);
    }
    private void SitDown()
    {
        if (GameManager.loadedScene == "UniClassroom")
        {
            transform.position = new Vector3(-4.68f, 0.36f, 0f);
        }
        else if (GameManager.loadedScene == "Cafe")
        {
            transform.position = new Vector3(-3.68f, -1.03f, -1.29f);
            transform.localScale = new Vector3(0.428f, 0.428f, 0.428f);
        }
        
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.SetBool("isSitting", true);
        //animator.Play("ANIM_Austin_Sit");
    }

    public void LeaveRoom()
    {
        SaveXCoordinate(transform.position.x);
        Debug.Log("position saved at " + transform.position);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            LeaveRoom();
        }
        if (collision.gameObject.tag == "Border")
        {
            animator.SetBool("isWalking", false);
            Debug.Log("Stopped Movement animation");
        }
    }

    private void OnDestroy()
    {
        LeaveRoom();
    }



    public void ReenterRoom()
    {
        Vector3 newPosition = startingPosition;
        newPosition.x = savedXCoordinate;
        transform.position = newPosition;
        Debug.Log("Reentered Room at " + transform.position);
    }




    // Save the character's X coordinate for the current room
    private void SaveXCoordinate(float x)
    {
        string roomKey = GetRoomKey();
        PlayerPrefs.SetFloat(roomKey, x);
        PlayerPrefs.Save();
    }




    // Load the saved X coordinate for the current room
    private float LoadSavedXCoordinate()
    {
        string roomKey = GetRoomKey();
        return PlayerPrefs.GetFloat(roomKey, startingPosition.x);
    }


    private bool HasSavedXCoordinate()
    {
        string roomKey = GetRoomKey();
        return PlayerPrefs.HasKey(roomKey);
    }



    // Generate a unique key for the current room
    private string GetRoomKey()
    {
        return $"{currentSceneName}_XCoordinate";
    }
}
