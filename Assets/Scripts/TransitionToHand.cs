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
    // Start is called before the first frame update
    void Start()
    {
        mainCamera.enabled = true;
        handCamera.enabled = false;
        austin.SetActive(true);
        hand.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.inRangeOfClothes && Input.GetKeyDown(KeyCode.E))
        {
            isHandMode = !isHandMode;

            mainCamera.enabled = !isHandMode;
            handCamera.enabled = isHandMode;

            austin.SetActive(!isHandMode);
            hand.SetActive(isHandMode);
        }

    }
}
