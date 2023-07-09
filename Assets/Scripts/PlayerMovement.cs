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
        if (HasSavedXCoordinate())
        {
            savedXCoordinate = LoadSavedXCoordinate();
            ReenterRoom();
        }
    }




    private void Update()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
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
        Debug.Log("Reentered Room");
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
