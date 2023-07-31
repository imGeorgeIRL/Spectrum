using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float shakeAmount = 0.05f;

    private Vector3 originalPosition;
    private bool isShaking = false;


    private void Awake()
    {
        originalPosition = transform.localPosition;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sensoryMetre >= 90f)
        {
            Shake();
        }
    }

    private void Shake()
    {
        if (!isShaking)
        {
            transform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;
        }
    }
}
