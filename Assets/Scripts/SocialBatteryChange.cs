using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SocialBatteryChange : MonoBehaviour
{
    public Slider slider;
    private float decreaseRate = 0.5f;

    public float increaseInterval;

    private bool isIncreasing = false;
    public DialogueManager dialogueManager;


    private void Update()
    {
        if (!isIncreasing && !dialogueManager.dialogueIsPlaying)
        {
            StartCoroutine(IncreaseSliderValue());
        }

        if (GameManager.loadedScene == "BusTerminal")
        {

        }
    }

    private IEnumerator IncreaseSliderValue()
    {
        isIncreasing = true;

        slider.value -= decreaseRate;
        yield return new WaitForSeconds(increaseInterval);

        isIncreasing = false;
    }
}
