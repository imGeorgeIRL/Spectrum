using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionToHand : MonoBehaviour
{
    public Camera mainCamera;
    public Camera handCamera;

    public GameObject austin;

    public GameObject hand;

    private bool isHandMode = false;
    private HandController controller;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera.enabled = true;
        handCamera.enabled = false;
        austin.SetActive(true);
        hand.SetActive(false);
        controller = hand.GetComponent<HandController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.inRangeOfClothes && Input.GetKeyDown(KeyCode.E) && !isHandMode)
        {
            //isHandMode = !isHandMode;

            //mainCamera.enabled = !isHandMode;
            //handCamera.enabled = isHandMode;

            //austin.SetActive(!isHandMode);
            //hand.SetActive(isHandMode);

            isHandMode = true;

            mainCamera.enabled = false;
            handCamera.enabled = true;

            austin.SetActive(false);
            hand.SetActive(true);
            //controller.TriggerDialogue();
        }
        else if (Input.GetKeyDown(KeyCode.E) && isHandMode)
        {
            isHandMode = false;

            mainCamera.enabled = true;
            handCamera.enabled = false;

            austin.SetActive(true);
            hand.SetActive(false);
        }

    }
}
