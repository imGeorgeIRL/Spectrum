using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueChange : MonoBehaviour
{
    public Slider slider;
    public float increaseRate;
    public float decreaseRate;

    public float increaseInterval;
    
    private bool isIncreasing = false;
    public DialogueManager dialogueManager;


    private void Update()
    {
        if (!isIncreasing && !dialogueManager.dialogueIsPlaying)
        {
            StartCoroutine(IncreaseSliderValue());
        }
    }

    private IEnumerator IncreaseSliderValue()
    {
        isIncreasing = true;

        slider.value += increaseRate;
        yield return new WaitForSeconds(increaseInterval);

        isIncreasing = false;
    }
}
