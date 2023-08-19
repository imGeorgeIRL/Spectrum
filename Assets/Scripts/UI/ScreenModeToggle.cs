using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenModeToggle : MonoBehaviour
{
    public Toggle fullscreenToggle;
    // Start is called before the first frame update
    void Awake()
    {
        fullscreenToggle.isOn = Screen.fullScreen;

        fullscreenToggle.onValueChanged.AddListener(ToggleScreenMode);
    }

    public void ToggleScreenMode(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        Debug.Log("Full Screen mode is" + isFullScreen);
    }
}
