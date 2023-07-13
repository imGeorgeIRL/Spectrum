using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkyManager : MonoBehaviour
{
    public GameObject daySky;
    public GameObject nightSky;

    

    //private SpriteRenderer skyRenderer;
    // Start is called before the first frame update
    void Start()
    {
        //daySky = new Color(182, 197, 229, 255);
        //nightSky = new Color(47, 56, 77, 255);
        //skyRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isDayTime)
        {
            daySky.SetActive(true);
            nightSky.SetActive(false);

        }
        else
        {
            nightSky.SetActive(true);
            daySky.SetActive(false);

        }
    }
}
