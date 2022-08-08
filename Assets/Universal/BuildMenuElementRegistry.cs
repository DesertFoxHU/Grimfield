using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
#if UNITY_SERVER && !UNITY_EDITOR
SceneManager.LoadScene(1, LoadSceneMode.Additive);
#endif
    }

    public BuildMenuElement Find(BuildingType type)
    {
        return elements.Find(x => x.BuildingType == type);
    }
}
