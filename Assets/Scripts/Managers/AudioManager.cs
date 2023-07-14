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

        
    }
    private void OnEnable()
    {
        string sceneName = GameManager.loadedScene;

        AudioClip clip = GetSceneClip(sceneName);

        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
    
    private AudioClip GetSceneClip(string sceneName)
    {
        switch (sceneName)
        {
            case "Bedroom":
                return musicClips[0];
                
            case "LivingRoom":
                if (!GameManager.transitionFromBedroom)
                {
                    return musicClips[0];
                }
                else
                {
                    return null;
                }
                           
                
            case "Outside":
                return musicClips[1];
                
            default:
                return null;
                
        }
    }
}
