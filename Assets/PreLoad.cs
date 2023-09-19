using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreLoad : MonoBehaviour
{
    [SerializeField] private GameObject preLoadPanel;
    // Start is called before the first frame update
    void Start()
    {
        
        Debug.Log("Preload is " + GameManager.preLoad);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.preLoad == 0)
        {
            preLoadPanel.SetActive(true);
        }
        else
        {
            preLoadPanel.SetActive(false);
        }
    }
}
