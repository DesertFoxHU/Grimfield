using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenuElementRegistry : MonoBehaviour
{
    public static List<BuildMenuElement> elements = new List<BuildMenuElement>();

    public List<TextAsset> ParseOnStart;

    public void Start()
    {
        foreach(TextAsset asset in ParseOnStart)
        {
            elements.Add(BuildMenuElement.LoadText(asset.text));
        }
        Debug.Log($"Loaded {elements.Count} elements!");
    }

    public BuildMenuElement Find(BuildingType type)
    {
        return elements.Find(x => x.BuildingType == type);
    }
}
