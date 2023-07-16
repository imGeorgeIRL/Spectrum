using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] musicClips;

   
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //string sceneName = GameManager.loadedScene;

        AudioClip clip = GetSceneClip();

        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    
    private AudioClip GetSceneClip()
    {
        Debug.Log("Scene Name: " + GameManager.loadedScene);
        Debug.Log("musicClips Length: " + musicClips.Length);
        AudioClip clip = null;

        switch (GameManager.loadedScene)
        {
            case "Bedroom":
                clip = musicClips[0];
                break;

            case "LivingRoom":
                if (!GameManager.transitionFromBedroom)
                {
                    clip = musicClips[0];
                }
                break;

            case "Outside":
                clip = musicClips[1];
                break;
        }

        return clip;
    }
}
