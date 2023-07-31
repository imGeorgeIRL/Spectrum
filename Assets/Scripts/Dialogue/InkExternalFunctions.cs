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
        story.BindExternalFunction("calendarInteract", (int cal) =>
        {
            if (cal == 1)
            {
                GameManager.interactedWithWardrobe = true;
                cal -= 1;
            }
            //if (cal == 0)
            //{
            //    GameManager.interactedWithWardrobe = false;
            //}
        });
        story.BindExternalFunction("tvChoice", (int choice) =>
        {
            if (choice == 1)
            {
                GameManager.tvChoice = 1;
                GameManager.spaceDoc = true;
                Debug.Log("TV Choice 1");
            }
            else if (choice == 2)
            {
                GameManager.tvChoice = 2;
                GameManager.news = true;
                Debug.Log("TV Choice 2");
            }
            else if (choice == 3)
            {
                GameManager.tvChoice = 3;
                GameManager.realityTv = true;
                Debug.Log("TV Choice 3");
            }
            choice = 0;
        });
        story.BindExternalFunction("talkedToMum", (int mum) =>
        {
            if (mum == 1)
            {
                GameManager.talkedToMum = true;
                mum -= 1;
            }
        });
        story.BindExternalFunction("calmDown", (int calm) =>
        {
            if (calm == 1)
            {
                GameManager.calmingDown = true;
                calm -= 1;
            }
        });
        story.BindExternalFunction("noahWalkAway", (int walk) =>
        {
            if (walk == 1)
            {
                GameManager.noahWalkAway = true;
                walk -= 1;
            }
        });
    }

    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("makeChoice");
        story.UnbindExternalFunction("busChosen");
        story.UnbindExternalFunction("spokeTo");
        story.UnbindExternalFunction("turnNight");
        story.UnbindExternalFunction("sittingDown");
        story.UnbindExternalFunction("bedTime");
        story.UnbindExternalFunction("watchTv");
        story.UnbindExternalFunction("timeSkip");
        story.UnbindExternalFunction("drMiller");
        story.UnbindExternalFunction("calendarInteract");
        story.UnbindExternalFunction("tvChoice");
        story.UnbindExternalFunction("talkedToMum");
        story.UnbindExternalFunction("calmDown");
        story.UnbindExternalFunction("noahWalkAway");
    }

    
}
