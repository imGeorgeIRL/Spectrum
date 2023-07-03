using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Animator animator;
    private Rigidbody2D rb;

    private bool isWalking = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
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
