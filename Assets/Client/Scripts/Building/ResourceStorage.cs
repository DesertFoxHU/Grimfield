using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStorage
{
    public AbstractBuilding owner;
    public ResourceType Type { get; private set; }
    public double Amount { get; private set; }
    public Dictionary<int, double> MaxAmountAtLevel { get; set; }

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
        Amount = amount;
        MaxAmountAtLevel = maxAmountAtLevel;
    }

    public void AddSafe(double amount)
    {
        if(Amount + amount > MaxAmountAtLevel[owner.Level])
        {
            Amount = MaxAmountAtLevel[owner.Level];
        }
        else
        {
            Amount += amount;
        }
    }
}
