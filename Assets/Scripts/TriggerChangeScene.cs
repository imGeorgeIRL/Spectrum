using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (GameManager.loadedScene == "OutsideObservatory")
        {
            SceneManager.UnloadSceneAsync(GameManager.loadedScene);
            GameManager.loadedScene = "Observatory";
            SceneManager.LoadSceneAsync("Observatory", LoadSceneMode.Additive);
            //StartCoroutine(WaitForLoad());


            GameManager.SaveSensoryMetre();
            GameManager.SaveSocialBattery();
        }
    }

}
