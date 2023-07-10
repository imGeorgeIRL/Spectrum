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

    private void Update()
    {
        if (!isIncreasing)
        {
            StartCoroutine(IncreaseSliderValue());
        }
    }

    private IEnumerator IncreaseSliderValue()
    {
        isIncreasing = true;

        if (increaseRate > 0f)
        {
            slider.value += increaseRate;
            yield return new WaitForSeconds(increaseInterval);
        }
        else if (decreaseRate > 0f)
        {
            slider.value -= decreaseRate;
            yield return new WaitForSeconds(increaseInterval);
        }
        isIncreasing = false;
    }
}
