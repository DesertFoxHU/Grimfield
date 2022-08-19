using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// For storing datas only relevant for this player
/// </summary>
public static class PlayerInfo
{
    public static Dictionary<ResourceType, double> Resources = new Dictionary<ResourceType, double>();

    public static void UpdateResource(ResourceType type, double amount)
    {
        if (Resources.ContainsKey(type))
        {
            Resources[type] = amount;
        }
        else Resources.Add(type, amount);

        BuildPanel.Instance.segments.ForEach(x => x.UpdateCostColor());
    }
}
