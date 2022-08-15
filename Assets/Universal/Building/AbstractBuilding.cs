using Newtonsoft.Json;
using ServerSide;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public abstract class AbstractBuilding
{
    [JsonIgnore] public ServerPlayer owner { get; private set; }
    public readonly Guid ID = Guid.NewGuid();
    public Vector3Int Position { get; private set; }
    public int Level { get; private set; }

    public AbstractBuilding(ServerPlayer owner, Vector3Int position)
    {
        this.owner = owner;
        Position = position;
        Level = 1;
    }

    public static Type GetClass(BuildingType type)
    {
        if (type == BuildingType.Village) return typeof(Village);
        else if (type == BuildingType.Forestry) return typeof(Forestry);
        else if (type == BuildingType.Orchard) return typeof(Orchard);
        else if (type == BuildingType.Quarry) return typeof(Quarry);
        else if (type == BuildingType.GoldMine) return typeof(GoldMine);

        else if (type == BuildingType.Barrack) return typeof(Barrack);
        else return null;
    }

    /// <summary>
    /// Called when every player had thier turn
    /// </summary>
    public virtual void OnTurnCycleEnded() { }

    public abstract BuildingType BuildingType { get; }

    public BuildingDefinition GetDefinition()
    {
        return DefinitionRegistry.Instance.Find(BuildingType);
    }
}
