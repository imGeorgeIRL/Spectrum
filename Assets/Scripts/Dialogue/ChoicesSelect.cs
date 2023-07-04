using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoicesSelect : MonoBehaviour
{
    private int choiceID;

    public GameObject[] choicesArray;
    // Start is called before the first frame update
    void Start()
    {
        choicesArray[choiceID].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            choicesArray[choiceID].SetActive(false);
            choiceID = (choiceID + 1) % choicesArray.Length;
            choicesArray[choiceID].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            choicesArray[choiceID].SetActive(false);
            choiceID--;
            if (choiceID < 0)
            {
                choiceID = choicesArray.Length - 1;
            }
            choicesArray[choiceID].SetActive(true);
        }
    }
}
