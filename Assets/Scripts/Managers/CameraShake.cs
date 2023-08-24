using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float shakeAmount = 0.05f;

    private Vector3 originalPosition;
    private bool isShaking = false;

    public float shakeDuration = 2f;
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
        if (GameManager.sensoryMetre >= 85f)
        {
            Shake();
        }
    }

    public void Shake()
    {
        if (!isShaking)
        {
            transform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;
        }
    }
    public void StopShake()
    {
        // Reset the camera's position to its original position
        transform.localPosition = originalPosition;
    }
    public void ShakeForDuration()
    {
        transform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;
    }

    public void ShakeCamera()
    {
        isShaking = true;

        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            // Generate a random offset within the specified shake amount
            Vector3 randomOffset = Random.insideUnitSphere * shakeAmount;

            // Apply the offset to the camera's position
            transform.localPosition = originalPosition + randomOffset;

            // Increment the elapsed time
            elapsed += Time.deltaTime;

        }

        // Reset the camera's position to its original position
        transform.localPosition = originalPosition;
        isShaking = false;
    }
}
