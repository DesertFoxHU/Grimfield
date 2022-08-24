using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI content;
    public LayoutElement layoutElement;
    public int characterWrapLimit;
    public RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Application.isEditor)
        {
            int titleLength = title.text.Length;
            int contentLength = content.text.Length;

            layoutElement.enabled = (titleLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;
        }

        Vector2 position = Input.mousePosition;
        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;
    }

    public void SetText(string title, string content)
    {
        if (string.IsNullOrEmpty(title))
        {
            this.title.gameObject.SetActive(false);
        }
        else
        {
            this.title.gameObject.SetActive(true);
            this.title.text = title;
        }

        this.content.text = content;

        int titleLength = this.title.text.Length;
        int contentLength = this.content.text.Length;

        layoutElement.enabled = (titleLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;
    }
}
