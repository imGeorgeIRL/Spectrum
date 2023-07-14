using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class RhythmGame : MonoBehaviour
{
    public Slider slider;
    private float sliderDecreaseAmount = 5f;
    public KeyCode inputKey = KeyCode.Space;

    private float ringSizeDecreaseSpeed = 0.1f;
    private float ringGreenDuration = 1f;
    
    public GameObject cueObject;
    public GameObject ringPrefab;

    private Coroutine currentScaleCoroutine;
    private Coroutine currentHideCoroutine;

    private bool isHidingCoroutineRunning = false;
    private bool hasPressedInput = false;

    private void Start()
    {
        cueObject.SetActive(false);
        slider.value = GameManager.sensoryMetre;
    }

    public void StartGame()
    {
        // Show the visual cue
        cueObject.SetActive(true);

        // Start the rhythm mechanic
        StartCoroutine(StartRhythmMechanic());
    }

    private IEnumerator StartRhythmMechanic()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 3f));

            GameObject currentRing = Instantiate(ringPrefab, transform.position, Quaternion.identity);

            currentScaleCoroutine = StartCoroutine(ScaleRing(currentRing));

            yield return currentScaleCoroutine;

            currentHideCoroutine = StartCoroutine(HideRing(currentRing));
            isHidingCoroutineRunning = true;
            yield return currentHideCoroutine;
            isHidingCoroutineRunning = false;
            hasPressedInput = false;
        }
    }

    private IEnumerator ScaleRing(GameObject ring)
    {
        float timer = 0f;
        bool isRingGreen = false;

        while (timer < ringGreenDuration)
        {
            // Decrease the ring's size
            ring.transform.localScale -= new Vector3(ringSizeDecreaseSpeed, ringSizeDecreaseSpeed, 0f) * Time.deltaTime;

            // Change color to green if it reaches a certain size
            if (ring.transform.localScale.x <= 0.5f && !isRingGreen)
            {
                ring.GetComponent<SpriteRenderer>().color = Color.green;
                isRingGreen = true;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        // Destroy the ring if it becomes too small
        while (ring.transform.localScale.x > 0.1f)
        {
            ring.transform.localScale -= new Vector3(ringSizeDecreaseSpeed, ringSizeDecreaseSpeed, 0f) * Time.deltaTime;
            yield return null;
        }

        Destroy(ring);
    }

    private IEnumerator HideRing(GameObject ring)
    {
        yield return new WaitForSeconds(ringGreenDuration);

        Destroy(ring);
    }

    private void Update()
    {
        if (!hasPressedInput && Input.GetKeyDown(inputKey) && !isHidingCoroutineRunning)
        {
            slider.value -= sliderDecreaseAmount;
            hasPressedInput = true;
        }
    }
}
