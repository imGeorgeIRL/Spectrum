using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class RhythmGame : MonoBehaviour
{
    public GameObject rhythmVisualCue;
    public Slider slider;

    private float ringSizeDecreaseSpeed = 0.2f;

    
    public GameObject cueObject;
    public GameObject ringPrefab;

    public GameObject[] starArray;

    private bool visualCueActive = false;

    private bool rhythmRunning = false;
    private bool isGreen = false;
    private bool hasSucceeded = false;
    private bool gameFinished = false;

    private float targetScale = 0.1f;
    private int successes = 0;

    private GameObject currentRing;
    private void Start()
    {
        rhythmVisualCue.SetActive(false);
        cueObject.SetActive(false);
        
        slider.value = GameManager.sensoryMetre;
        foreach (GameObject star in starArray)
        {
            star.SetActive(false);
        }
        successes = 0;
    }

    public void StartGame()
    {
        rhythmVisualCue.SetActive(false);
        // Show the visual cue
        cueObject.SetActive(true);
        hasSucceeded = false;
        gameFinished = false;

        // Start the rhythm mechanic
        StartCoroutine(StartRhythmMechanic());
    }

    private IEnumerator StartRhythmMechanic()
    {
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        if (!rhythmRunning && !gameFinished)
        {
            //hasSucceeded = false;
            GameObject ring = Instantiate(ringPrefab, transform.position, Quaternion.identity);
            currentRing = ring;
            StartCoroutine(ScaleRing(ring));
        }
    }

    private IEnumerator ScaleRing(GameObject ring)
    { //decrease the ring size
        Vector3 initialScale = ring.transform.localScale;
        
        while (ring.transform.localScale.x > targetScale && ring.transform.localScale.y > targetScale)
        {
            ring.transform.localScale -= new Vector3(ringSizeDecreaseSpeed, ringSizeDecreaseSpeed, 0f) * Time.deltaTime;

            if (ring.transform.localScale.x <= 0.4f && !isGreen)
            {
                
                isGreen = true;
                ring.GetComponent<SpriteRenderer>().color = Color.green;
                StartCoroutine(ResetColorAndCheckSuccess(ring));
            }
            if (ring.transform.localScale.x <= 0.3f && isGreen)
            {
                isGreen = false;
                ring.GetComponent<SpriteRenderer>().color = Color.white;
            }
            
            yield return null;
        }

        Destroy(ring);

        if (successes < 3 && !gameFinished)
        {
            StartCoroutine(StartRhythmMechanic());
        }

    }

    private IEnumerator ResetColorAndCheckSuccess(GameObject ring)
    {
        yield return new WaitForSeconds(1f);

        if (successes == 3)
        {
            // Reduce sensory metre or perform desired action
            StopCoroutine(StartRhythmMechanic());
        }
        else
        {
            //ResetSuccesses();
        }
    }
 
    private void Update()
    {
        if (GameManager.sensoryMetre > 75f)
        {
            if (!visualCueActive)
            {
                rhythmVisualCue.SetActive(true);
                visualCueActive = true;
            }
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                
                StartGame();

            }

        }

        if (Input.GetKeyDown(KeyCode.Space) && isGreen && !hasSucceeded)
        {
            Success();

            if (successes == 3)
            {
                // Reduce sensory metre or perform desired action
                gameFinished = true;
                cueObject.SetActive(false);
                StartCoroutine(SuccessWin());
                //StopCoroutine(StartRhythmMechanic());
            }
        }
        if (successes == 0)
        {
            foreach (GameObject star in starArray)
            {
                star.SetActive(false);
            }
        }
        else if (successes == 1)
        {
            starArray[0].SetActive(true);
        }
        else if (successes == 2)
        {
            starArray[1].SetActive(true);
        }
        else if(successes == 3)
        {
            starArray[2].SetActive(true);
        }
    }
    private IEnumerator ResetHasSucceeded()
    {
        yield return new WaitForSeconds(2f);
        hasSucceeded = false;
    }
    private void Success()
    {
        successes++;
        currentRing.GetComponent<SpriteRenderer>().color = Color.white;
        isGreen = false;
        Debug.Log("Success! succeses are: " + successes);
        hasSucceeded = true;
        if(successes < 3)
        {
            StartCoroutine(ResetHasSucceeded());
        }

    }
    //private void ResetSuccesses()
    //{
    //    successes = 0;
    //   // hasSucceeded = false;
    //}

    private IEnumerator SuccessWin()
    {
        successes = 0;
        StopCoroutine(StartRhythmMechanic());
        yield return new WaitForSeconds(1f);
               
        GameManager.sensoryMetre -= 20f;
        visualCueActive = false;
        
    }
}
