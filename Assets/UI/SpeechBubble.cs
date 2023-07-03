using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class SpeechBubble : MonoBehaviour
{
    public Text textComponent;
    public float padding = 10f;

    private RectTransform bubbleRectTransform;

    private void Awake()
    {
        bubbleRectTransform = GetComponent<RectTransform>();
    }

    public void UpdateText(string text)
    {
        textComponent.text = text;
        ResizeBubble();
    }

    private void ResizeBubble()
    {
        Vector2 bubbleSize = new Vector2(textComponent.preferredWidth + padding, textComponent.preferredHeight + padding);
        bubbleRectTransform.sizeDelta = bubbleSize;
    }
}
