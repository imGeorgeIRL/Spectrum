using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanicText : MonoBehaviour
{
    public GameObject[] textArray;

    private bool coroutinePlaying;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject text in textArray)
        {
            text.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isHavingMeltdown 
            && !coroutinePlaying 
            && GameManager.loadedScene != "UniClassroom")
        {
            StartCoroutine(PanicTextDisplay());
        }
        if (GameManager.safeZoneActive)
        {
            StopCoroutine(PanicTextDisplay());
        }
    }

    private IEnumerator PanicTextDisplay()
    {
        coroutinePlaying = true;
        GameObject text = textArray[Random.Range(0, textArray.Length)];
        text.SetActive(true);
        yield return new WaitForSeconds(3);
        text.SetActive(false);
        yield return new WaitForSeconds(1);
        coroutinePlaying = false;
    }
}
