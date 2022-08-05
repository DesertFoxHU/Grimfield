using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStorage
{
    public AbstractBuilding owner;
    public ResourceType Type { get; private set; }
    private double SafeAmount = 0;
    public Dictionary<int, double> MaxAmountAtLevel { get; set; } = new Dictionary<int, double>();

    public double Amount
    {
        get => SafeAmount;
        set 
        {
            if (MaxAmountAtLevel[owner.Level - 1] > value)
            {
                SafeAmount = MaxAmountAtLevel[owner.Level - 1];
            }
            else
            {
                SafeAmount = value;
            }
        }
    }

    public ResourceStorage(AbstractBuilding owner, ResourceType type, Dictionary<int, double> maxAmountAtLevel)
    {
        this.owner = owner;
        Type = type;
        MaxAmountAtLevel = maxAmountAtLevel;
    }

    public ResourceStorage(AbstractBuilding owner, ResourceType type, double amount, Dictionary<int, double> maxAmountAtLevel)
    {
        this.owner = owner;
        Type = type;
        SafeAmount = amount;
        MaxAmountAtLevel = maxAmountAtLevel;
    }
}
