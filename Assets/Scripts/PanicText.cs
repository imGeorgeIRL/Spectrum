using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanicText : MonoBehaviour
{
    //public GameObject[] textArray;

    public GameObject panic;

    //private bool coroutinePlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        //foreach (GameObject text in textArray)
        //{
        //    text.SetActive(false);
        //}

        panic.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.startPanic)
        {
            panic.SetActive(true);
            //if (!GameManager.isTalking && !coroutinePlaying)
            //{
            //    coroutinePlaying = true;
            //    StartCoroutine(PanicTextDisplay());
            //}
            //else
            //{
            //    foreach (GameObject text in textArray)
            //    {
            //        text.SetActive(false);
            //    }
            //}
        }
        if (GameManager.safeZoneActive)
        {
            //StopCoroutine(PanicTextDisplay());
            GameManager.startPanic = false;
            panic.SetActive(false);
            return;
        }
    }

    //private IEnumerator PanicTextDisplay()
    //{
    //    GameObject text = textArray[Random.Range(0, textArray.Length)];
    //    text.SetActive(true);
    //    yield return new WaitForSeconds(3);
    //    text.SetActive(false);
    //    yield return new WaitForSeconds(1);
    //    coroutinePlaying = false;
    //}
}
