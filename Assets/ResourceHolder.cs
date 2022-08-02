using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
