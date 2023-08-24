using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public Image fadeImage; // Reference to the black overlay image
    public float fadeSpeed = 1.0f; // Speed of the fading

    private bool isFading = false;
    private float alpha = 0.0f;

    private void Start()
    {
        // Initialize the alpha value of the image
        alpha = fadeImage.color.a;
    }

    public void StartFade()
    {
        isFading = true;
    }

    private void Update()
    {
        if (isFading)
        {
            // Increase or decrease alpha based on fadeSpeed
            alpha += fadeSpeed * Time.deltaTime;

            // Clamp alpha between 0 and 1
            alpha = Mathf.Clamp01(alpha);

            // Update the image color
            Color newColor = fadeImage.color;
            newColor.a = alpha;
            fadeImage.color = newColor;

            // Check if the fade is complete
            if (alpha >= 1.0f)
            {
                isFading = false;
            }
        }
    }
}
