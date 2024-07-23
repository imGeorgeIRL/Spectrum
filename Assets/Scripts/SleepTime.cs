using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using PixelCrushers.DialogueSystem;

public class SleepTime : MonoBehaviour
{
    private float fadeDuration = 3f;
    public AnimationCurve fadeCurve;

    private CanvasGroup canvasGroup;

    public TextMeshProUGUI dayText;
    private string day;

    private bool coroutinePlaying;
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
        GameManager.tuesdayMeltdown = false;
        Debug.LogWarning("Day of the week is " + day);
        dayText.text = day;
        yield return new WaitForSeconds(5f);
        DialogueLua.SetVariable("isDay", true);
        //GameManager.goToSleep = false;
        canvasGroup.alpha = 0f;
    }

    private void Update()
    {
        bool bedTime = DialogueLua.GetVariable("BedTime").asBool;
        if (bedTime && !coroutinePlaying)
        {
            coroutinePlaying = true;
            GameManager.dayOfWeek += 1;
            StartCoroutine(NightToDay());
            GameManager.interactedWithWardrobe = false;
            DialogueLua.SetVariable("BedTime", false);
            
            if (!bedTime)
            {
                Debug.Log("Bed time is now False");
            }
        }
    }

    public void BedTime()
    {
        if (!coroutinePlaying)
        {
            coroutinePlaying = true;
            GameManager.dayOfWeek += 1;
            StartCoroutine(NightToDay());
            GameManager.interactedWithWardrobe = false;
            DialogueLua.SetVariable("BedTime", false);
            
            if (DialogueLua.GetVariable("BedTime", false))
            {
                Debug.Log("Bed time is now False");
            }
        }
    }
}
