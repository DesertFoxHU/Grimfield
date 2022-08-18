using ServerSide;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forestry : AbstractBuilding, IResourceStorage
{
    public Forestry(ServerPlayer owner, Vector3Int position) : base(owner, position) 
    {
        BuildingStorage = new List<ResourceStorage>
        {
            new ResourceStorage(this, ResourceType.Wood, new Dictionary<int, double>()
            {
                { 1, 5d }
            })
        };
    }

    public override BuildingType BuildingType => BuildingType.Forestry;

    public List<ResourceStorage> Storage => BuildingStorage;

    public List<ResourceStorage> BuildingStorage;

    public override void OnTurnCycleEnded()
    {
        double remained = Storage[0].AddSafe(GetDefinition().ProduceLevel.Find(x => x.level == Level).value);
        if(remained > 0)
        {
            owner.TryStoreResource(GetDefinition().produceType, remained);
        }
    }
}
