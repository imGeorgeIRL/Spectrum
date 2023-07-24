using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueChange : MonoBehaviour
{
    public Slider slider;
    private float increaseRate = 0.7f;

    private float increaseInterval = 3f;
    
    private bool isIncreasing = false;
    public DialogueManager dialogueManager;


    private void Update()
    {
        if (!isIncreasing && !dialogueManager.dialogueIsPlaying)
        {
            StartCoroutine(IncreaseSliderValue());
        }

        if (GameManager.loadedScene == "BusTerminal")
        {
            increaseInterval = 2f;
        }


        
    }

    private IEnumerator IncreaseSliderValue()
    {
        isIncreasing = true;
        if(GameManager.socialBattery <=100f && GameManager.socialBattery > 50)
        {
            if (GameManager.safeZoneActive)
            {
                increaseRate = -1f;
            }
            else
            {
                if (GameManager.loadedScene == "Bedroom" || GameManager.loadedScene == "LoungeKitchen")
                {
                    increaseRate = -1f;
                }
                else
                {
                    increaseRate = 0.7f;
                }
            }
        }
        else if (GameManager.socialBattery <=50 && GameManager.socialBattery > 25)
        {
            if (GameManager.safeZoneActive)
            {
                increaseRate = -1f;
            }
            else
            {
                if (GameManager.loadedScene == "Bedroom" || GameManager.loadedScene == "LoungeKitchen")
                {
                    increaseRate = -1f;
                }
                else
                {
                    increaseRate = 1f;
                }
            }
        }
        else if (GameManager.socialBattery <= 25)
        {
            if (GameManager.safeZoneActive)
            {
                increaseRate = -1f;
            }
            else
            {
                if (GameManager.loadedScene == "Bedroom" || GameManager.loadedScene == "LoungeKitchen")
                {
                    increaseRate = -1.5f;
                }
                else
                {
                    increaseRate = 1.3f;
                }
            }
        }
        GameManager.sensoryMetre += increaseRate;
        GameManager.sensoryMetre = Mathf.Clamp(GameManager.sensoryMetre, 0, 100);
        yield return new WaitForSeconds(increaseInterval);

        isIncreasing = false;
    }
}
