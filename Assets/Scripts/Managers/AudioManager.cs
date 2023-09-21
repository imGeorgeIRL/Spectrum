using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private string previousScene;
    public AudioSource audioSource;

    public AudioClip nightMusic;

    private AudioLowPassFilter lowPassFilter;
    private AudioEchoFilter echoFilter;

    private bool isFading = false;
    private float fadeDuration = 1.0f;

    private bool isPanicMusicPlaying = false;

    [System.Serializable]
    public class SceneAudioClip
    {
        public string sceneName;
        public AudioClip audioClip;
    }

    public AudioClip panicAudio;
    public AudioClip deepConvoAudio;

    public List<SceneAudioClip> sceneAudioClips = new List<SceneAudioClip>();
    private Dictionary<string, AudioClip> audioClipMap = new Dictionary<string, AudioClip>();

    public static float musicVolume = 1f;
    

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
                break;
        }

        if (GameManager.isHavingMeltdown && !isPanicMusicPlaying && GameManager.loadedScene == "Cafe")
        {
            isPanicMusicPlaying = true; 
            audioSource.Stop();
            audioSource.clip = panicAudio;
            audioSource.Play();
            audioSource.volume = (musicVolume * 2);
            audioSource.loop = true;
        }
        if (GameManager.safeZoneActive && GameManager.isHavingMeltdown && isPanicMusicPlaying)
        {
            isPanicMusicPlaying = false;
            audioSource.Stop();
            audioSource.clip = deepConvoAudio;
            audioSource.Play();
            audioSource.volume = (musicVolume * 2);
            audioSource.loop = true;
        }
        
    }

    private IEnumerator CrossfadeToNewClip(AudioClip newClip, float fadeDuration)
    {
        isFading = true;
        float startVolume = audioSource.volume;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, timer / fadeDuration);
            yield return null;
        }

        audioSource.Stop();

        // Play the new clip
        audioSource.clip = newClip;
        audioSource.Play();

        timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, musicVolume, timer / fadeDuration);
            yield return null;
        }

        isFading = false;
    }


    private void PlaySceneAudio(string sceneName)
    {
        
        if (audioClipMap.ContainsKey(sceneName))
        {
            AudioClip audioClip = audioClipMap[sceneName];

            if (!GameManager.isDayTime && GameManager.loadedScene == "Outside")
            {
                audioClip = nightMusic;
            }

                
            if (audioClip != null)
            {
                // Check for special cases where music continues between scenes
                if (ShouldMusicContinue(previousScene, sceneName))
                {
                    // Music continues, no need to change the audio clip
                    return;
                }
                if (!isFading)
                {
                    // Stop the previous clip if it's playing and fade it out
                    StartCoroutine(CrossfadeToNewClip(audioClip, fadeDuration));
                }
                

                // Play the new clip
                //audioSource.clip = audioClip;
                //audioSource.Play();
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
        if (previousScene == "Cafe" && currentScene == "TownCentre" && GameManager.isHavingMeltdown)
        {
            return true;
        }
        if ((previousScene == "AfterObservatory" && currentScene == "Credits") || (previousScene == "Credits" && currentScene == "AfterObservatory"))
        {
            return true;
        }
        return false;
    }
}
