using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueChange : MonoBehaviour
{
    public Slider slider;
    private float increaseRate = 1f;

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
            increaseRate = 1.5f;
        }
    }

    private IEnumerator IncreaseSliderValue()
    {
        isIncreasing = true;

        GameManager.sensoryMetre += increaseRate;
        yield return new WaitForSeconds(increaseInterval);

        isIncreasing = false;
    }
}
