using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuSlider : MonoBehaviour
{
    public Slider musicVolSlider;
    public Slider sfxVolSlider;
    // Start is called before the first frame update
    void Start()
    {
        musicVolSlider.value = AudioManager.musicVolume;
        sfxVolSlider.value = SFXManager.sfxVolume;

        musicVolSlider.onValueChanged.AddListener(ChangeMusicVolume);
        sfxVolSlider.onValueChanged.AddListener(ChangeSFXVolume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMusicVolume(float newMusicVol)
    {
        AudioManager.musicVolume = newMusicVol;
    }
    public void ChangeSFXVolume(float newSfxVol)
    {
        SFXManager.sfxVolume = newSfxVol;
    }

}
