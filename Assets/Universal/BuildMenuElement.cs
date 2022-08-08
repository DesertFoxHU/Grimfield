using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class BuildMenuElement
{
    public string Title;
    public string Description;
    public BuildPanel.Category Category;
    public BuildingType BuildingType;
    public List<ResourceHolder> ResourceCost;
    public List<ResourceHolder> IncreasePerBuy;

    public Dictionary<ResourceType, double> GetBuildingCost(int boughtCount)
    {
        Dictionary<ResourceType, double> cost = new Dictionary<ResourceType, double>();
        foreach (ResourceHolder holder in ResourceCost)
        {
            if (!cost.ContainsKey(holder.type))
            {
                cost.Add(holder.type, holder.Value);
            }
            else cost[holder.type] += holder.Value;
        }

        if(boughtCount != 0)
        {
            foreach (ResourceHolder holder in IncreasePerBuy)
            {
                if (!cost.ContainsKey(holder.type))
                {
                    cost.Add(holder.type, holder.Value * boughtCount);
                }
                else cost[holder.type] += holder.Value * boughtCount;
            }
        }
        return cost;
    }

    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(BuildMenuElement));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }

    public static BuildMenuElement Load(string path)
    {
        var serializer = new XmlSerializer(typeof(BuildMenuElement));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as BuildMenuElement;
        }
    }

    public static BuildMenuElement LoadText(string text)
    {
        var serializer = new XmlSerializer(typeof(BuildMenuElement));
        using (var stream = new System.IO.StringReader(text))
        {
            return serializer.Deserialize(stream) as BuildMenuElement;
        }
    }
}
