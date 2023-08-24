using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class RhythmGame : MonoBehaviour
{
    public GameObject rhythmVisualCue;
    public Slider sensorySlider;
    public Slider socialSlider;

    private float ringSizeDecreaseSpeed = 0.3f;

    
    public GameObject cueObject;
    public GameObject ringPrefab;

    public GameObject[] starArray;
    public GameObject[] dialogueTriggerArray;

    private bool visualCueActive = false;

    private bool rhythmRunning = false;
    private bool isGreen = false;
    private bool hasSucceeded = false;
    private bool gameFinished = false;

    private float targetScale = 0.1f;
    private int successes = 0;

    private GameObject currentRing;

    private void Awake()
    {
        rhythmVisualCue.SetActive(false);
        cueObject.SetActive(false);        
    }
    private void Start()
    {        
        sensorySlider.value = GameManager.sensoryMetre;
        socialSlider.value = GameManager.socialBattery;
        foreach (GameObject star in starArray)
        {
            star.SetActive(false);
        }
        successes = 0;
    }

    public void StartGame()
    {
        foreach (GameObject trigger in  dialogueTriggerArray)
        {
            trigger.SetActive(false);
        }
        GameManager.rhythmActive = true;
        // Show the visual cue
        cueObject.SetActive(true);
        hasSucceeded = false;
        gameFinished = false;
        rhythmRunning = true;
        // Start the rhythm mechanic
        StartCoroutine(StartRhythmMechanic());
    }

    private IEnumerator StartRhythmMechanic()
    {
        rhythmVisualCue.SetActive(false);
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
        if (!rhythmRunning)
        {
            foreach (GameObject star in starArray)
            {
                star.SetActive(false);
            }
        }
        if (GameManager.isTalking)
        {
            rhythmVisualCue.SetActive(false);
        }
        if (!GameManager.tuesdayMeltdown)
        {
            if (GameManager.isHavingMeltdown && !GameManager.isTalking)
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
        }
        else
        {
            rhythmVisualCue.SetActive(false);
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
               
        GameManager.sensoryMetre -= 40f;
        rhythmVisualCue.SetActive(false);
        cueObject.SetActive(false);
        visualCueActive = false;
        foreach (GameObject trigger in dialogueTriggerArray)
        {
            trigger.SetActive(true);
        }
        GameManager.rhythmActive = false;
    }
}
