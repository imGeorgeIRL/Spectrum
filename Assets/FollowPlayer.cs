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
        // Calculate the target position with the offset
        targetPos.x = player.position.x - followDistance * Mathf.Sign(player.position.x - transform.position.x);
        targetPos.y = transform.position.y;

        // Apply the delayed movement with smooth dampening
        transform.position = Vector2.SmoothDamp(transform.position, targetPos, ref currentVelocity, delay, followSpeed);


        float horizontalDifference = player.position.x - transform.position.x;

        if (Mathf.Abs(horizontalDifference) > (followDistance + 0.2f))
        {
            animator.SetBool("Walking", true);
            animator.SetFloat("Direction", Mathf.Sign(horizontalDifference));
        }
        else
        {
            animator.SetBool("Walking", false);
            animator.SetFloat("Direction", 0);
        }
    }
}
