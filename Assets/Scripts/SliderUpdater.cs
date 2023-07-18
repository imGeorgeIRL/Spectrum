using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUpdater : MonoBehaviour
{

    public Slider sensoryMetreSlider;

    public Slider socialBatterySlider;


    private void UpdateSliderValue()
    {
        sensoryMetreSlider.value = GameManager.sensoryMetre;
        socialBatterySlider.value = GameManager.socialBattery;
    }


    // Update is called once per frame
    void Update()
    {
        UpdateSliderValue();

    }

    public void SaveSensoryMetre()
    {
        GameManager.sensoryMetre = sensoryMetreSlider.value; // Update the sensory meter value from the slider
        GameManager.SaveSensoryMetre(); // Save the sensory meter value to PlayerPrefs
    }

    public void SaveSocialBattery()
    {
        GameManager.socialBattery = socialBatterySlider.value;
        GameManager.SaveSocialBattery();
    }

}
