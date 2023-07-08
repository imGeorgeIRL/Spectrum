using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUpdater : MonoBehaviour
{

    public Slider slider;

    private void UpdateSliderValue()
    {
        slider.value = GameManager.sensoryMetre;
    }


    // Update is called once per frame
    void Update()
    {
        UpdateSliderValue();
    }

    public void SaveSensoryMeter()
    {
        GameManager.sensoryMetre = slider.value; // Update the sensory meter value from the slider
        GameManager.SaveSensoryMetre(); // Save the sensory meter value to PlayerPrefs
    }
}
