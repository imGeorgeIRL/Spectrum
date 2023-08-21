using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    public float scrollSpeed = 2.0f;
    public float loadMainMenuY = 10.0f;

    private void Start()
    {
        StartCoroutine(WaitForCredits());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
    }

    private IEnumerator WaitForCredits()
    {
        yield return new WaitForSeconds(60);
        SceneManager.LoadScene("MainMenu");
    }
}
