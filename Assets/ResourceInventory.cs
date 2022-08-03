using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceInventory
{
    public ResourceHolder[] holder;

    public ResourceInventory()
    {
        holder = new ResourceHolder[Enum.GetValues(typeof(ResourceHolder.Type)).Length];
        int index = 0;
        foreach(ResourceHolder.Type item in Enum.GetValues(typeof(ResourceHolder.Type)))
        {
            holder[index] = new ResourceHolder(item, 0);
            index++;
        }
    }

    public ResourceHolder Find(ResourceHolder.Type type)
    {
        foreach(ResourceHolder hold in holder)
        {
            if (hold.type == type) return hold;
        }
        return null;
    }

    public void SetAmount(ResourceHolder.Type type, double amount)
    {
        Find(type).Value = amount;
    }

    public void AddAmount(ResourceHolder.Type type, double amount)
    {
        Find(type).Value += amount;
    }
}
