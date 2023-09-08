using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteboardVisibility : MonoBehaviour
{
    public GameObject[] whiteboards;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.whiteboardInactive)
        {
            foreach (GameObject whiteboard in whiteboards)
            {
                whiteboard.SetActive(false);
            }
        }
    }

    public void OnDestroy()
    {
        GameManager.whiteboardInactive = false;
    }
}
