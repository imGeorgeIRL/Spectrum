using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SocialBatteryChange : MonoBehaviour
{
    public Slider slider;
    private float decreaseRate = 0.6f;

    public float increaseInterval;

    private bool isIncreasing = false;
    public DialogueManager dialogueManager;


    private void Update()
    {
        if (!isIncreasing)
        {
            StartCoroutine(IncreaseSliderValue());
        }

        if (GameManager.loadedScene == "Bedroom" || GameManager.loadedScene == "LoungeKitchen")
        {
            decreaseRate = -1f;
        }

        if (GameManager.safeZoneActive)
        {
            decreaseRate = -1f;
        }
    }

    private IEnumerator IncreaseSliderValue()
    {
        isIncreasing = true;

        GameManager.socialBattery -= decreaseRate;
        GameManager.socialBattery = Mathf.Clamp(GameManager.socialBattery, 0, 100);
        yield return new WaitForSeconds(increaseInterval);

        isIncreasing = false;
    }
}
