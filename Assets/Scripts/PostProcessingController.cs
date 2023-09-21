using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingController : MonoBehaviour
{
   // public Camera mainCamera;
    //private float cameraZoom;
    public PostProcessVolume postProcessVol;

    private float vignetteIntensity = 0.3f;
    private float vignetteSmoothness = 0.7f;

    private float grainIntensity = 0.5f;
    private float grainSize = 1.7f;

    // Start is called before the first frame update
    void Start()
    {
        postProcessVol = GetComponent<PostProcessVolume>();
        

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sensoryMetre >= 80f && GameManager.sensoryMetre <= 85)
        {
            UpdatePostProcessingSettings();
        }
        else if (GameManager.sensoryMetre >= 85)
        {
            MeltdownPostProcessing();
        }
        else if (!GameManager.isDayTime &&
            (GameManager.loadedScene == "Outside"||
            GameManager.loadedScene == "TownCentre" ||
            GameManager.loadedScene == "UniEntrance"))
        {
            NightPostProcessing();
        }
        else
        {
            OldPostProcessingSettings();
           // mainCamera.orthographicSize = 6f;
            //mainCamera.transform.position = new Vector3(transform.position.x, 0.41f, transform.position.z);
        }

        

    }

    public void NightPostProcessing()
    {
        if (postProcessVol == null)
        {
            return;
        }

        PostProcessProfile profile = postProcessVol.profile;

        if (profile == null)
        {
            return;
        }

        if (profile.TryGetSettings(out ColorGrading color))
        {
            float r = 77f / 255f;
            float g = 111f / 255f;
            float b = 159f / 255f;

            // Set the HDR color filter value
            color.colorFilter.value = new Color(r, g, b, 1.0f);
        }
    }
    
    public void MeltdownPostProcessing()
    {
        if (postProcessVol == null)
        {
            return;
        }

        PostProcessProfile profile = postProcessVol.profile;

        if (profile == null)
        {
            return;
        }

        if (profile.TryGetSettings(out ChromaticAberration chromatic))
        {
            chromatic.intensity.value = 0.3f;
        }

        if (profile.TryGetSettings(out Vignette vignette))
        {
            vignette.intensity.value = vignetteIntensity;
            vignette.smoothness.value = vignetteSmoothness;
        }

        if (profile.TryGetSettings(out Grain grain))
        {
            grain.intensity.value = grainIntensity;
            grain.size.value = grainSize;
        }

    }
    public void UpdatePostProcessingSettings()
    {
        if (postProcessVol == null)
        {
            return;
        }

        PostProcessProfile profile = postProcessVol.profile;

        if (profile == null)
        {
            return;
        }

        if (profile.TryGetSettings(out Vignette vignette))
        {
            vignette.intensity.value = vignetteIntensity;
            vignette.smoothness.value = vignetteSmoothness;
        }
        
        if (profile.TryGetSettings(out Grain grain))
        {
            grain.intensity.value = grainIntensity;
            grain.size.value = grainSize;
        }

        if (profile.TryGetSettings(out ChromaticAberration chromatic))
        {
            chromatic.intensity.value = 0f;
        }

    }

    public void OldPostProcessingSettings()
    {
        if (postProcessVol == null)
        {
            return;
        }

        PostProcessProfile profile = postProcessVol.profile;

        if (profile == null)
        {
            return;
        }

        if (profile.TryGetSettings(out Vignette vignette))
        {
            vignette.intensity.value = 0.38f;
            vignette.smoothness.value = 0.23f;
        }

        if (profile.TryGetSettings(out Grain grain))
        {
            grain.intensity.value = 0.17f;
            grain.size.value = 0.91f;
        }
        if (profile.TryGetSettings(out ChromaticAberration chromatic))
        {
            chromatic.intensity.value = 0f;
        }
        if (profile.TryGetSettings(out ColorGrading color))
        {
            color.colorFilter.value = Color.white;
        }
    }

}
