using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStorage
{
    public ResourceType Type { get; private set; }
    public double Amount { get; set; } = 0;
    public Dictionary<int, double> MaxAmountAtLevel { get; set; } = new Dictionary<int, double>();

    public ResourceStorage(ResourceType type, Dictionary<int, double> maxAmountAtLevel)
    {
        Type = type;
        MaxAmountAtLevel = maxAmountAtLevel;
    }

    public ResourceStorage(ResourceType type, double amount, Dictionary<int, double> maxAmountAtLevel)
    {
        Type = type;
        Amount = amount;
        MaxAmountAtLevel = maxAmountAtLevel;
    }
}
