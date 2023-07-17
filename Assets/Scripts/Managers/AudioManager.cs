using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip clip;

   
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();


        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    private void OnDestroy()
    {
        audioSource.Stop();
    }
}
