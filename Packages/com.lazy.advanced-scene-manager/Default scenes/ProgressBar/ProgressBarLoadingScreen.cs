using System.Collections;
using AdvancedSceneManager.Utility;
using UnityEngine.UI;

namespace AdvancedSceneManager.Defaults
{

    /// <summary>A default loading screen script. Displays progress with a progress bar.</summary>
    public class ProgressBarLoadingScreen : FadeLoadingScreen
    {

        public Slider slider;

        public override IEnumerator OnOpen() =>
            FadeIn();

        public override IEnumerator OnClose()
        {

            //Hide slider before fade, since it is brighter than background and will 
            //appear to stay on screen for longer than background which looks bad
            yield return slider.Fade(0, 0.5f, true);

            yield return FadeOut();

        }

        public override void OnProgressChanged(float progress)
        {
            if (slider)
                slider.value = progress;
        }

    }

}
