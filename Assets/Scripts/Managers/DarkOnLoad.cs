using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkOnLoad : MonoBehaviour
{
    public GameObject darkScreen;
    // Start is called before the first frame update
    void Start()
    {
        darkScreen.SetActive(true);
        StartCoroutine(Loading());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Loading()
    {

        yield return new WaitForSeconds(2f);
        darkScreen.SetActive(false);
    }
}
