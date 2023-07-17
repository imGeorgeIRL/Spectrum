using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BlackScreen : MonoBehaviour
{
    private float fadeDuration = 3f;
    public AnimationCurve fadeCurve;

    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
    }

    private IEnumerator FadeCanvasGroup(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float normalizedTime = elapsedTime / fadeDuration;
            float currentAlpha = Mathf.Lerp(startAlpha, endAlpha, fadeCurve.Evaluate(normalizedTime));

            canvasGroup.alpha = currentAlpha;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
    }

    private IEnumerator BlackScreenEnable()
    {
        canvasGroup.alpha = 1f;
        yield return new WaitForSeconds(1f);
        canvasGroup.alpha = 0f;
    }

    private IEnumerator NightToDay()
    {
        yield return new WaitForSeconds(4f);
        yield return StartCoroutine(FadeCanvasGroup(0f, 1f));
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("EndOfPlaytest");
        //GameManager.goToSleep = false;
        //GameManager.isDayTime = true;

        //yield return new WaitForSeconds(1.5f);
        //yield return StartCoroutine(FadeCanvasGroup(1f, 0f));
    }

    private void Update()
    {
        if (GameManager.goToSleep)
        {
            GameManager.goToSleep = false; // Set to false immediately to avoid multiple coroutine calls
            StartCoroutine(NightToDay());
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(BlackScreenEnable());
        }
    }
}
