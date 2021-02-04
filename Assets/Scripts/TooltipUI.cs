using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipUI : MonoBehaviour
{
    private RectTransform rectTransform;
    [SerializeField] private RectTransform canvasRectTransform;
    private TextMeshProUGUI textMeshPro;
    private RectTransform backgroundRectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        textMeshPro = transform.Find("text").GetComponent<TextMeshProUGUI>();
        backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();

        SetText("kebab");
    }

    private void Update()
    {
      rectTransform.anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;
    }

    private void SetText(string tooltipText)
    {
        textMeshPro.SetText(tooltipText);

        textMeshPro.ForceMeshUpdate(); // bo odswierzanie czasem swiruje
        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(8, 8);
        backgroundRectTransform.sizeDelta = textSize + padding;
    }
}
