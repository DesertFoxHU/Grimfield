using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Like a BuildMenuElement but it's attached to one of them to make logic
/// </summary>
public class BuildMenuSegment : MonoBehaviour
{
    public Image Icon;
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Description;

    public BuildMenuElement LastLoaded { get; private set; }

    public void Load(BuildMenuElement element)
    {
        LastLoaded = element;
        Title.text = LastLoaded.Title;
        Description.text = LastLoaded.Description;
    }
}
