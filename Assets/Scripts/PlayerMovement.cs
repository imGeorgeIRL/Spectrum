using System.Collections;
using System.Collections.Generic;
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

    public Animation leftIdle;
    public Animation rightIdle;
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


    private void Update()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            animator.SetBool("isWalking", false);
            return;
        }
        else
        {
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Move the character horizontally
        Vector2 movement = new Vector2(moveHorizontal * moveSpeed, rb.velocity.y);
        rb.velocity = movement;

        
            if (moveHorizontal != 0f)
            {
                animator.SetBool("isWalking", true);
                animator.SetFloat("Direction", moveHorizontal);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
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
