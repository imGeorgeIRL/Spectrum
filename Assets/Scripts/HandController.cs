using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public float handMoveSpeed = 3f;
    private Animator anim;

    private bool goodTexture;
    private bool badTexture;
    private void Start()
    {
        anim = GetComponent<Animator>();
        goodTexture = false;
        badTexture = false; 
    }
    private void Update()
    {
        // Hand movement logic
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
        transform.Translate(moveDirection * handMoveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Grab", true);
            if (goodTexture)
            {
                //do fun stuff with visuals
                Debug.LogWarning("good");
            }
            else if (badTexture)
            {
                //do not so fun stuff with visuals or screen shake??
                Debug.LogWarning("bad");
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("Grab", false);
        }


        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered!");
        if (other.CompareTag("GoodTexture"))
        {
            Debug.Log("This is a good texture :)");
            goodTexture = true;
        }
        if (other.CompareTag("BadTexture"))
        {
            Debug.Log("YEEEEEEEOWCH bad texture :C");
            badTexture = true;
        }
    }
}
