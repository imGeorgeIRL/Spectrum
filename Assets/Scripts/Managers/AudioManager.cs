using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private string previousScene;
    public AudioSource audioSource;
    private AudioLowPassFilter lowPassFilter;
    private AudioEchoFilter echoFilter;

    [System.Serializable]
    public class SceneAudioClip
    {
        public string sceneName;
        public AudioClip audioClip;
    }

    public AudioClip panicAudio;
    public List<SceneAudioClip> sceneAudioClips = new List<SceneAudioClip>();
    private Dictionary<string, AudioClip> audioClipMap = new Dictionary<string, AudioClip>();

    public static float musicVolume = 0.35f;
    

    //************************************************************************\\
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
        foreach (SceneAudioClip sceneAudioClip in sceneAudioClips)
        {
            audioClipMap[sceneAudioClip.sceneName] = sceneAudioClip.audioClip;
        }
    }

    private void Start()
    {
        string loadedScene = GameManager.loadedScene;
        PlaySceneAudio(loadedScene);

        previousScene = loadedScene;
        audioSource = GetComponent<AudioSource>();
        lowPassFilter = GetComponent<AudioLowPassFilter>();
        echoFilter = GetComponent<AudioEchoFilter>();
    }

    private void Update()
    {
        string currentScene = GameManager.loadedScene;
        if (currentScene != previousScene)
        {
            PlaySceneAudio(currentScene);
            previousScene = currentScene;
        }
        if (GameManager.watchingTv)
        {
            audioSource.volume = 0f;
        }
        else
        {
            audioSource.volume = musicVolume;
        }

        if (GameManager.safeZoneActive)
        {
            audioSource.volume = (musicVolume / 3);
            lowPassFilter.enabled = true;
            echoFilter.enabled = true;
        }
        else
        {
            audioSource.volume = musicVolume;
            lowPassFilter.enabled = false;
            echoFilter.enabled = false;
        }

        switch (currentScene)
        {
            case "Cafe":
                audioSource.volume = (musicVolume);
                break;
            default:
                Debug.LogWarning("No case for " + currentScene);
                break;
        }
    }

    private void PlaySceneAudio(string sceneName)
    {
        if (GameManager.sensoryMetre >= 85f)
        {
            audioSource.Stop();
            audioSource.clip = panicAudio;
            audioSource.Play();
            audioSource.loop = true;
            return;
        }
        if (audioClipMap.ContainsKey(sceneName))
        {
            AudioClip audioClip = audioClipMap[sceneName];
            if (audioClip != null)
            {
                // Check for special cases where music continues between scenes
                if (ShouldMusicContinue(previousScene, sceneName))
                {
                    // Music continues, no need to change the audio clip
                    return;
                }

                // Stop the previous clip if it's playing
                audioSource.Stop();

                // Play the new clip
                audioSource.clip = audioClip;
                audioSource.Play();
                audioSource.loop = true;
                return;
            }
        }

        Debug.LogWarning("Audio clip not found for scene: " + sceneName);
    }

    private bool ShouldMusicContinue(string previousScene, string currentScene)
    {
        // Check the specific cases where music should continue between scenes
        if ((previousScene == "Bedroom" && currentScene == "LoungeKitchen") ||
            (previousScene == "LoungeKitchen" && currentScene == "Bedroom"))
        {
            return true;
        }
        if ((previousScene == "UniEntrance" && currentScene == "UniClassroom") ||
            (previousScene == "UniClassroom" && currentScene == "UniEntrance"))
        {
            return true;
        }
        return false;
    }
}
