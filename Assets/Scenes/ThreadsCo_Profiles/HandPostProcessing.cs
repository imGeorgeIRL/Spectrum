using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class HandPostProcessing : MonoBehaviour
{
    private ParticleSystem particleSysten;

    public PostProcessVolume postProcessVol;

    public GameObject cam;
    private CameraShake camShake;

    private bool canShake;
    private float shakeAmount = 0.05f;

    private Vector3 originalPosition;
    private bool calledCoroutine = false;
    // Start is called before the first frame update
    void Start()
    {
        particleSysten = GetComponentInChildren<ParticleSystem>();
        camShake = cam.GetComponent<CameraShake>();
        originalPosition = cam.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.goodTexture && !calledCoroutine)
        {
            GoodPostProcessing();
            particleSysten.Play();
            GameManager.goodTexture = false;
            StartCoroutine(TurnOff());
        }
        
        if (GameManager.badTexture && !calledCoroutine)
        {
            BadPostProcessing();
            camShake.ShakeForDuration();
            StartCoroutine(TurnOffFast());
            GameManager.badTexture = false;
        }
        
        if (canShake)
        {
            cam.transform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;
        }
    }

    public void GoodPostProcessing()
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

        if (profile.TryGetSettings(out Bloom bloom))
        {
            bloom.enabled.value = true;
        }

        if (profile.TryGetSettings(out DepthOfField depth))
        {
            depth.enabled.value = true;
        }
    }

    public void BadPostProcessing()
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

        if (profile.TryGetSettings(out Grain grain))
        {
            grain.enabled.value = true;
        }

        if (profile.TryGetSettings(out ChromaticAberration chromaticAberration))
        {
            chromaticAberration.enabled.value = true;
        }
        if(profile.TryGetSettings(out Vignette vignette))
        {
            vignette.enabled.value = true;
        }
    }

    public void NoPostProcessing()
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

        if (profile.TryGetSettings(out Bloom bloom))
        {
            bloom.enabled.value = false;
        }

        if (profile.TryGetSettings(out DepthOfField depth))
        {
            depth.enabled.value = false;
        }

        if (profile.TryGetSettings(out Grain grain))
        {
            grain.enabled.value = false;
        }

        if (profile.TryGetSettings(out ChromaticAberration chromaticAberration))
        {
            chromaticAberration.enabled.value = false;
        }
        if (profile.TryGetSettings(out Vignette vignette))
        {
            vignette.enabled.value = false;
        }
    }

    private IEnumerator TurnOff()
    {
        calledCoroutine = true;
        yield return new WaitForSeconds(3);
        NoPostProcessing();
        calledCoroutine = false;
    }
    private IEnumerator TurnOffFast()
    {
        canShake = true;
        calledCoroutine = true;
        
        yield return new WaitForSeconds(1);
        //camShake.StopShake();
        NoPostProcessing();
        calledCoroutine = false;
        canShake = false;
    }
}
