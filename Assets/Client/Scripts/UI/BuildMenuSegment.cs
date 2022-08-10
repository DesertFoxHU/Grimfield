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
    [System.Serializable]
    public struct ResourceIcons
    {
        public ResourceType type;
        public Sprite icon;
    }

    public Image Icon;
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Description;
    public GameObject CostHolder;
    public GameObject CostPrefab;
    public List<ResourceIcons> Icons;

    public BuildMenuElement LastLoaded { get; private set; }
    public BuildingType Type
    {
        get => LastLoaded.BuildingType;
    }

    public void Load(BuildMenuElement element)
    {
        LastLoaded = element;
        Title.text = LastLoaded.Title;
        Description.text = LastLoaded.Description;
        Icon.sprite = DefinitionRegistry.Instance.Find(element.BuildingType).GetSpriteByLevel(1);
        RenderCost(element, 0);
    }

    public void RenderCost(int boughtCount)
    {
        RenderCost(LastLoaded, boughtCount);
    }

    public void RenderCost(BuildMenuElement element, int boughtCount)
    {
        foreach (Transform children in CostHolder.transform) Destroy(children.gameObject);

        Dictionary<ResourceType, double> cost = element.GetBuildingCost(boughtCount);

        cost.RemoveAll(val => val <= 0);
        int count = 1;
        foreach (ResourceType type in cost.Keys)
        {
            Vector3 pos = new Vector3(0, 0, 0);
            if (count % 2 == 0) pos.y = -12;
            else pos.y = 12;

            pos.x = 120 * ((int) ((count-1)/2));

            GameObject costObject = Instantiate(CostPrefab, pos, Quaternion.identity);
            costObject.transform.SetParent(CostHolder.transform, false);
            costObject.name = type.ToString();
            costObject.GetComponent<Image>().sprite = Icons.Find(x => x.type == type).icon;
            costObject.GetComponentInChildren<TextMeshProUGUI>().text = "" + cost[type];

            count++;
        }
    }

    public void StartBlueprintMode()
    {
        FindObjectOfType<Blueprinting>().ChangeBlueprint(DefinitionRegistry.Instance.Find(LastLoaded.BuildingType));
    }
}
