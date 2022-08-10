using ServerSide;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forestry : AbstractBuilding, IProducer, IResourceStorage
{
    public Forestry(Vector3Int position) : base(position) 
    {
        BuildingStorage = new List<ResourceStorage>
        {
            new ResourceStorage(this, ResourceType.Wood, new Dictionary<int, double>()
            {
                { 1, 3d }
            })
        };
    }

    public override BuildingType BuildingType => BuildingType.Forestry;

    public ResourceType Type => ResourceType.Wood;

    public Dictionary<int, double> ProduceLevel => new Dictionary<int, double>()
    {
        { 1, 0.25 },
        { 2, 0.5 },
        { 3, 1.25 },
        { 4, 1.5 },
        { 5, 2 }
    };

    public List<ResourceStorage> Storage => BuildingStorage;

    public List<ResourceStorage> BuildingStorage;

    public override void OnTurnCycleEnded(ServerPlayer owner)
    {
        double remained = Storage[0].AddSafe(ProduceLevel[this.Level]);
        if(remained > 0)
        {
            owner.TryStoreResource(Type, remained);
        }
    }
}
