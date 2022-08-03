using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AbstractBuilding
{
    public Vector3Int Position { get; private set; }

    protected AbstractBuilding(Vector3Int position)
    {
        Position = position;
    }

    public abstract BuildingType BuildingType { get; }

    public abstract List<TileType> PlaceableOn { get; }
}
