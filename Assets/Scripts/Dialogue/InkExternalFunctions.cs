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
        story.BindExternalFunction("spokeTo", (int person) =>
        {
            if (person == 1)
            {
                GameManager.sensoryMetre += 5f;
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
        story.BindExternalFunction("dailyTasks", (int task) =>
        {
            if (task == 1)
            {
                GameManager.eatenBreakfast = true;
            }
            if (task == 2)
            {
                GameManager.goneToUni = true;
            }
            if (task == 3)
            {
                GameManager.madeAFriend = true;
            }
        });
        story.BindExternalFunction("sittingDown", (int sit) =>
        {
            if (sit == 1)
            {
                GameManager.isSitting = true;
            }
            else if (sit == 1)
            {
                GameManager.isSitting = false;
            }
        });
    }

    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("makeChoice");
        //Debug.Log("Choice made was " + GameManager.choiceSelected);
        story.UnbindExternalFunction("spokeTo");
        story.UnbindExternalFunction("turnNight");
    }

}
