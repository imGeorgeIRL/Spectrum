using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightDialogue : MonoBehaviour
{
    public GameObject tvDialogueTrigger;

    // Start is called before the first frame update
    void Start()
    {
        tvDialogueTrigger.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isDayTime)
        {
            if (GameManager.watchingTv)
            {
                tvDialogueTrigger.SetActive(false);
            }
            else
            {
                tvDialogueTrigger.SetActive(true);
            }

        }
        else
        {
            tvDialogueTrigger.SetActive(false);
        }
    }
}
