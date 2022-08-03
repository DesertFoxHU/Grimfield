using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AbstractBuilding
{
    public Vector3Int Position { get; private set; }
    public int Level { get; private set; }

    public AbstractBuilding(Vector3Int position)
    {
        Position = position;
        Level = 1;
    }

    public static Type GetClass(BuildingType type)
    {
        if (type == BuildingType.Village) return typeof(Village);
        else return null;
    }

    public abstract BuildingType BuildingType { get; }
}
