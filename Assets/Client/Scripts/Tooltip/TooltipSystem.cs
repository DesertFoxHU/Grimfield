using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem Instance;

    public Tooltip tooltip;

    private void Awake()
    {
        Instance = this;
    }

    public static void Show(string title, string content)
    {
        Instance.tooltip.SetText(title, content);
        Instance.tooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        Instance.tooltip.gameObject.SetActive(false);
    }
}
