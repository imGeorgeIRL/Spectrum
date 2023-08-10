using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public float handMoveSpeed = 3f;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        // Hand movement logic
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
        transform.Translate(moveDirection * handMoveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetBool("Grab", true);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            anim.SetBool("Grab", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GoodTexture"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("This is a good texture :)");
            }
        }
        if (other.CompareTag("BadTexture"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("YEEEEEEEOWCH bad texture :C");
            }
        }
    }
}
