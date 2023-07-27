using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BlackScreen : MonoBehaviour
{
    private float fadeDuration = 3f;
    public AnimationCurve fadeCurve;

    private CanvasGroup canvasGroup;

    public TextMeshProUGUI dayText;
    private string day;
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        dayText.text = "";
        
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
        dayText.text = "";
        yield return new WaitForSeconds(2f);
        canvasGroup.alpha = 1f;
        GameManager.socialBattery = 100f;
        GameManager.sensoryMetre = 0f;
        yield return new WaitForSeconds(1f);
        // text Monday
        switch (GameManager.dayOfWeek)
        {
            case 0:
                day = "Monday";
                break;

            case 1:
                day = "Tuesday";
                break;

            case 2:
                day = "Wednesday";
                break;

            case 3:
                day = "Thursday";
                break;

            case 4:
                day = "Friday";
                break;

            default:
                Debug.LogWarning("No case for this day");
                break;
        }
        Debug.LogWarning("Day of the week is " + day);
        dayText.text = day;
        yield return new WaitForSeconds(5f);
        GameManager.isDayTime = true;
        canvasGroup.alpha = 0f;
    }

    private void Update()
    {
        if (GameManager.goToSleep)
        {
            GameManager.goToSleep = false; // Set to false immediately to avoid multiple coroutine calls
            GameManager.dayOfWeek += 1;
            StartCoroutine(NightToDay());
            GameManager.interactedWithWardrobe = false;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(BlackScreenEnable());
        }
    }
}
