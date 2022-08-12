using ServerSide;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orchard : AbstractBuilding
{
    public Orchard(Vector3Int position) : base(position) 
    {
    }

    public override BuildingType BuildingType => BuildingType.Orchard;

    public ResourceType Type => ResourceType.Food;

    public override void OnTurnCycleEnded(ServerPlayer owner)
    {
        owner.TryStoreResource(GetDefinition().produceType, GetDefinition().ProduceLevel.Find(x => x.level == Level).value);
    }
}
