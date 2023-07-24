using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingController : MonoBehaviour
{

    public PostProcessVolume postProcessVol;

    private float vignetteIntensity;
    private float grainIntensity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sensoryMetre >= 80f)
        {

        }
    }
}
