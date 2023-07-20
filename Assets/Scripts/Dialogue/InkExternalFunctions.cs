using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class InkExternalFunctions
{
    public void Bind(Story story)
    {
        story.BindExternalFunction("makeChoice", (int choice) =>
        {
            if (choice == 1)
            {
                GameManager.choiceSelected = 1;
                Debug.Log("Choice 1");
            }
            else if (choice == 2)
            {
                GameManager.choiceSelected = 2;
                Debug.Log("Choice 2");
            }
            else if(choice == 3)
            {
                GameManager.choiceSelected = 3;
                Debug.Log("Choice 3");
            }
        });
        story.BindExternalFunction("busChosen", (int bus) =>
        {
            if (bus == 1)
            {
                GameManager.choiceSelected = 1;
                Debug.Log("Choice 1");
            }
            else if (bus == 2)
            {
                GameManager.choiceSelected = 2;
                Debug.Log("Choice 2");
            }
            else if (bus == 3)
            {
                GameManager.choiceSelected = 3;
                Debug.Log("Choice 3");
            }
        });
        story.BindExternalFunction("spokeTo", (int person) =>
        {
            if (person == 1)
            {
                GameManager.sensoryMetre += 5f;
                GameManager.socialBattery -= 5f;
                person -= 1;
            }
        });
        story.BindExternalFunction("turnNight", (int night) =>
        {
            if (night == 0)
            {
                GameManager.isDayTime = true;
            }
            else if (night == 1)
            {
                GameManager.isDayTime = false;
            }
        });
        
        story.BindExternalFunction("sittingDown", (int sit) =>
        {
            if (sit == 1)
            {
                GameManager.isSitting = true;
            }
            else if (sit == 0)
            {
                GameManager.isSitting = false;
            }
        });

        story.BindExternalFunction("bedTime", (int value) =>
        {
            if (value == 1)
            {
                GameManager.goToSleep = true;
                Debug.Log("going to sleep");
                value -= 1;
            }

        });
        story.BindExternalFunction("watchTv", (int watched) =>
        {
            if (watched == 1)
            {
                GameManager.watchingTv = true;
                watched -= 1;
            }

        });
        story.BindExternalFunction("timeSkip", (int skip) =>
        {
            if (skip == 1)
            {
                GameManager.timeSkip = true;
                skip -= 1;
            }

        });
        story.BindExternalFunction("drMiller", (int interact) =>
        {
            if (interact == 1)
            {
                GameManager.spokenToMiller = true;
                
            }

        });
    }

    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("makeChoice");
        story.UnbindExternalFunction("busChosen");
        //Debug.Log("Choice made was " + GameManager.choiceSelected);
        story.UnbindExternalFunction("spokeTo");
        story.UnbindExternalFunction("turnNight");
        story.UnbindExternalFunction("sittingDown");
        story.UnbindExternalFunction("bedTime");
        story.UnbindExternalFunction("watchTv");
        story.UnbindExternalFunction("timeSkip");
    }

    
}
