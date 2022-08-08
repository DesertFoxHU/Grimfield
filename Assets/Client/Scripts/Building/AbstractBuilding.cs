using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AbstractBuilding
{
    public readonly Guid ID = Guid.NewGuid();
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
        else if (type == BuildingType.Forestry) return typeof(Forestry);
        else if (type == BuildingType.Orchard) return typeof(Orchard);
        else return null;
    }

    /// <summary>
    /// Called when every player had thier turn
    /// </summary>
    public virtual void OnTurnCycleEnded() { }

    public abstract BuildingType BuildingType { get; }
}
