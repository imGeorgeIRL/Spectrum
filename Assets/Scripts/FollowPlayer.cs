using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Animator animator;

    public Transform player;
    private float followSpeed = 4f;
    private float delay = 1f;
    private float followDistance = 2f;

    private bool isFollowing = false;

    private Vector2 targetPos;
    private Vector2 currentVelocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalDifference = player.position.x - transform.position.x;

        if (Mathf.Abs(horizontalDifference) > followDistance)
        {
            // Calculate the target position with the offset
            targetPos.x = player.position.x - followDistance * Mathf.Sign(horizontalDifference);
            targetPos.y = transform.position.y;

            float offset = 0.1f; // Default offset

            if (animator.GetFloat("Direction") < 0) // If the Direction parameter is negative (moving left)
            {
                offset = -0.1f; // Set a negative offset for left movement
            }

            // Apply the delayed movement with smooth dampening
            transform.position = Vector2.SmoothDamp(transform.position, targetPos + new Vector2(offset, 0), ref currentVelocity, delay, followSpeed);

            if (!isFollowing) //set isFollowing to the opposite of whatever it is currently
            {
                isFollowing = true;
                animator.SetBool("Walking", true);
            }
            animator.SetFloat("Direction", Mathf.Sign(horizontalDifference));
        }
        else
        {
            // Stop the movement
            currentVelocity = Vector2.zero;
            if (isFollowing)
            {
                isFollowing = false;
                animator.SetBool("Walking", false);
            }
            animator.SetFloat("Direction", Mathf.Sign(horizontalDifference));
        }

        // Set the "Direction" parameter based on the horizontal difference
    }
}
