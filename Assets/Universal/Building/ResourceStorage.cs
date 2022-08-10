using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStorage
{
    public AbstractBuilding owner;
    public ResourceType Type { get; private set; }
    public double Amount { get; set; }
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

    /// <summary>
    /// Additive method with considering the limit of max amount
    /// </summary>
    /// <param name="amount">How many got actually added</param>
    /// <returns></returns>
    public double AddSafe(double amount)
    {
        double oldAmount = Amount;
        if(Amount + amount > MaxAmountAtLevel[owner.Level])
        {
            Amount = MaxAmountAtLevel[owner.Level];
            return MaxAmountAtLevel[owner.Level] - oldAmount;
        }
        else
        {
            Amount += amount;
            return 0;
        }
    }
}
