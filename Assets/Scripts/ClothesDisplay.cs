using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesDisplay : MonoBehaviour
{
    private BoxCollider2D bx;
    public GameObject visualCue;
    // Start is called before the first frame update
    void Start()
    {
        bx = GetComponent<BoxCollider2D>();
        visualCue.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.inRangeOfClothes = true;
        visualCue.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameManager.inRangeOfClothes = false;
        visualCue.SetActive(false);
    }
}
