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

    public GameObject austin;
    private RhythmGame rhythmGame;

    private void Start()
    {
        rhythmGame = austin.GetComponent<RhythmGame>();
    }
    private void Update()
    {
        if (!isIncreasing)
        {
            StartCoroutine(IncreaseSliderValue());
        }

        if (GameManager.loadedScene == "BusTerminal")
        {
            increaseInterval = 2f;
        }

        //if the sensory metre is higher than 90, then austin has a meltdown. 
        if (GameManager.sensoryMetre >= 85)
        {
            GameManager.isHavingMeltdown = true;
        }
        else
        {
            GameManager.isHavingMeltdown = false;
        }
        
        //if austin is havinga meltdown and the loaded scene is cafe, then deactivate the rhythm game
        if(GameManager.loadedScene == "Cafe")
        {
            GameManager.rhythmDeactivate = true;
            if (GameManager.isHavingMeltdown)
            {
                GameManager.canMoveWhileMeltdown = true;
            }
        }
        if (GameManager.loadedScene == "TownCentre")
        {
            if (GameManager.tuesdayMeltdown)
            {
                if (GameManager.safeZoneActive)
                {
                    rhythmGame.enabled = true;
                    //GameManager.tuesdayMeltdown = false;
                }
                else
                {
                    rhythmGame.enabled = false;
                }
            }
            else
            {
                rhythmGame.enabled = true;
            }
        }

        if (GameManager.rhythmDeactivate)
        {
            rhythmGame.enabled = false;
        }
        else
        {
            rhythmGame.enabled = true;
        }
    }

    private IEnumerator IncreaseSliderValue()
    {

        isIncreasing = true;
        if (GameManager.socialBattery <= 100f && GameManager.socialBattery > 50)
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
        else if (GameManager.socialBattery <= 50 && GameManager.socialBattery > 25)
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

        if (GameManager.dayOfWeek == 1 && GameManager.loadedScene == "UniClassroom")
        {
            if (GameManager.calmingDown)
            {
                increaseRate = 0.7f;
            }
            else
            {
                if (GameManager.sensoryMetre >= 91f)
                {
                    increaseRate = 0f;
                }
                else
                {
                    increaseRate = 3f;
                }
            }
        }

        if (GameManager.dayOfWeek == 1 && GameManager.loadedScene == "Cafe")
        {
            if (GameManager.sensoryMetre >= 85)
            {
                increaseRate = 0f;
                GameManager.tuesdayMeltdown = true;
            }
            else
            {
                increaseRate = 3f;
            }
        }

        GameManager.sensoryMetre += increaseRate;
        GameManager.sensoryMetre = Mathf.Clamp(GameManager.sensoryMetre, 0, 100);
        yield return new WaitForSeconds(increaseInterval);

        isIncreasing = false;
    }
}
