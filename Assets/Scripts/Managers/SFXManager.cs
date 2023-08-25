using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    private string previousScene;
    public AudioSource audioSource;

    [System.Serializable]
    public class SceneAudioClip
    {
        public string sceneName;
        public AudioClip audioClip;
    }

    public List<SceneAudioClip> sfxClips = new List<SceneAudioClip>();
    private Dictionary<string, AudioClip> sfxClipMap = new Dictionary<string, AudioClip>();

    public static float sfxVolume = 1.0f;
    private float clipVol;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        foreach (SceneAudioClip sfxClip in sfxClips)
        {
            sfxClipMap[sfxClip.sceneName] = sfxClip.audioClip;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        string loadedScene = GameManager.loadedScene;
        PlaySceneSFX(loadedScene);

        previousScene = loadedScene;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        string currentScene = GameManager.loadedScene;
        //PlaySceneSFX(currentScene);

        if (currentScene != previousScene)
        {
            audioSource.Stop();
            PlaySceneSFX(currentScene);
            previousScene = currentScene;
        }
        audioSource.volume = clipVol;
        switch (currentScene)
        {
            case "Outside":
                clipVol = (sfxVolume * 1);
                break;
            case "UniClassroom":
                clipVol = (sfxVolume * 0.5f);
                break;
            case "TownCentre":
                clipVol = (sfxVolume * 0.65f);
                break;
            case "UniEntrance":
                clipVol = (sfxVolume * 1);
                break;
            case "Cafe":
                clipVol = (sfxVolume * 1.3f);
                break;
            case "AfterObservatory":
                clipVol = (sfxVolume * 0.6f);
                break;
            default:
                clipVol = (sfxVolume * 1);
                break;
        }
    }

    private void PlaySceneSFX(string sceneName)
    {
        if (sfxClipMap.ContainsKey(sceneName))
        {
            AudioClip sfx = sfxClipMap[sceneName];
            if (sfx != null)
            {
                

                audioSource.clip = sfx;
                audioSource.Play();
                audioSource.loop = true;
                return;
            }
        }
    }
}
