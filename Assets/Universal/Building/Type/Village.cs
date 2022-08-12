using ServerSide;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Village : AbstractBuilding, IProducer, IResourceStorage
{
    public Village(Vector3Int position) : base(position) 
    {
        BuildingStorage = new List<ResourceStorage>
        {
            new ResourceStorage(this, ResourceType.Citizen, new Dictionary<int, double>()
            {
                { 1, 4d }
            }),
            new ResourceStorage(this, ResourceType.Food, new Dictionary<int, double>()
            {
                { 1, 10d }
            }),
            new ResourceStorage(this, ResourceType.Wood, new Dictionary<int, double>()
            {
                { 1, 10d }
            }),
            new ResourceStorage(this, ResourceType.Stone, new Dictionary<int, double>()
            {
                { 1, 10d }
            }),
            new ResourceStorage(this, ResourceType.Gold, new Dictionary<int, double>()
            {
                { 1, 10d }
            }),
            new ResourceStorage(this, ResourceType.Coin, new Dictionary<int, double>()
            {
                { 1, 10d }
            }),
        };
    }

    public bool IsCapital { get; private set; } = false;

    public override BuildingType BuildingType => BuildingType.Village;

    public ResourceType Type => ResourceType.Citizen;

    public Dictionary<int, double> ProduceLevel => new Dictionary<int, double>()
    {
        { 1, 0.5 },
        { 2, 1 },
        { 3, 1.5 },
        { 4, 2 },
        { 5, 2.5 }
    };

    public List<ResourceStorage> Storage => BuildingStorage;

    public List<ResourceStorage> BuildingStorage;

    public override void OnTurnCycleEnded(ServerPlayer owner)
    {
        double remained = Storage[0].AddSafe(ProduceLevel[this.Level]);
        if (remained > 0)
        {
            owner.TryStoreResource(Type, remained);
        }
    }
}
