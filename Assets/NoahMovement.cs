using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoahMovement : MonoBehaviour
{
    [SerializeField] private GameObject austin;
    [SerializeField] private GameObject wall;
    private BoxCollider2D wallCollider;
    private float moveSpeed = 4f;
    private float stoppingDistance = 3f;

    private Rigidbody2D rb;
    private Animator anim;

    private bool isMoving = false;

    private bool noahCanMove = true;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        anim = GetComponent<Animator>();

        wallCollider = wall.GetComponentInChildren<BoxCollider2D>();
    }

    private IEnumerator WaitForAustin()
    {
        yield return new WaitForSeconds(8);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isHavingMeltdown && noahCanMove)
        {
            MoveTowardsAustin();

        }

        if (isMoving)
        {
            anim.Play("Noah_Walk_Right");
            wallCollider.enabled = false;
        }
        else
        {
            anim.Play("Noah_Idle_Right");
            wallCollider.enabled = true;
        }

        if(Input.GetKeyDown(KeyCode.M))
        {
            GameManager.isHavingMeltdown = true;
        }
    }

    private void MoveTowardsAustin()
    {
        Vector2 direction = austin.transform.position - transform.position;

        direction.y = 0;

        float distance = direction.magnitude;

        if (distance > stoppingDistance)
        {
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
}
