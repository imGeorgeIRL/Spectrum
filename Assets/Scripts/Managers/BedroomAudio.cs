using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BedroomAudio : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip clip;


    private bool isPlaying;
    private bool canPlay;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isPlaying = false;

        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
            isPlaying = true;
        }
    }

    private void Update()
    {
        switch (GameManager.loadedScene)
        {
            case "Outside":
                
                break;
        }
    }

    private void OnDestroy()
    {
        GameObject.DontDestroyOnLoad(this);
    }
}
