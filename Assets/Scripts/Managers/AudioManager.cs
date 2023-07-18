using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private string previousScene;
    public AudioSource audioSource;

    [System.Serializable]
    public class SceneAudioClip
    {
        public string sceneName;
        public AudioClip audioClip;
    }

    public List<SceneAudioClip> sceneAudioClips = new List<SceneAudioClip>();
    private Dictionary<string, AudioClip> audioClipMap = new Dictionary<string, AudioClip>();

    

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
    }

    private void Update()
    {
        string currentScene = GameManager.loadedScene;
        if (currentScene != previousScene)
        {
            PlaySceneAudio(currentScene);
            previousScene = currentScene;
        }
    }

    private void PlaySceneAudio(string sceneName)
    {
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
