using System.Collections;
using AdvancedSceneManager.Utility;
using Lazy.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace AdvancedSceneManager.Defaults
{

    /// <summary>A default splash screen script. Fades splash screen in and out.</summary>
    [ExecuteAlways]
    [AddComponentMenu("")]
    public class SplashFade : Callbacks.SplashScreen
    {

        public CanvasGroup groupBackground;
        public Image background;

        void Start()
        {
            //Use same color as unity splash screen, if enabled, defaults to black otherwise
            background.color = SceneManager.app.startupProps?.effectiveFadeColor ?? Color.black;
        }

        public override IEnumerator OnOpen()
        {
            yield return groupBackground.Fade(1, 1).StartCoroutine();
        }

        public override IEnumerator OnClose()
        {
            yield return groupBackground.Fade(0, 1f).StartCoroutine();
        }

    }

}
