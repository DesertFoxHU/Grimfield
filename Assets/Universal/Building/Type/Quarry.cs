using ServerSide;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quarry : AbstractBuilding
{
    public Quarry(Vector3Int position) : base(position) 
    {
    }

    public override BuildingType BuildingType => BuildingType.Quarry;

    public override void OnTurnCycleEnded(ServerPlayer owner)
    {
        owner.TryStoreResource(GetDefinition().produceType, GetDefinition().ProduceLevel.Find(x => x.level == Level).value);
    }
}
