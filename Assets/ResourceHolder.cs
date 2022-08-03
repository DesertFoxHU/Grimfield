using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ResourceHolder is capable of storing a single type of Resource, but its not suitable for holding all Resource type
/// Instead use ResourceInventory for tracking every Resource
/// </summary>
public class ResourceHolder
{
    public enum Type
    {
        Citizen,
        Wood,
        Stone,
        Gold,
        Coin
    }

    public Type type;
    public double Value;

    public ResourceHolder()
    {
    }

    public ResourceHolder(Type type, double value)
    {
        this.type = type;
        Value = value;
    }
}
