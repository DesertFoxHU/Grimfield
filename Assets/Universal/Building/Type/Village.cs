using ServerSide;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class Village : AbstractBuilding, IResourceStorage
{
    public Village(ServerPlayer owner, Vector3Int position) : base(owner, position) 
    {
        BuildingStorage = new List<ResourceStorage>
        {
            new ResourceStorage(this, ResourceType.Citizen, new Dictionary<int, double>()
            {
                { 1, 5d }
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

        if (owner != null && owner.isFirstPlace)
        {
            IsCapital = true;
            owner.isFirstPlace = false;
        }
    }

    public bool IsCapital { get; private set; } = false;

    public override BuildingType BuildingType => BuildingType.Village;

    public List<ResourceStorage> Storage => BuildingStorage;

    private List<ResourceStorage> BuildingStorage;

    public override void OnClaimLand(Tilemap map)
    {
        OnClaimLand(map, 5);
    }

    public override void OnTurnCycleEnded()
    {
        double produce = GetDefinition().ProduceLevel.Find(x => x.level == Level).value;
        produce -= Storage[0].AddSafe(produce);
        if (produce > 0) //Remained some resource
        {
            owner.TryStoreResource(GetDefinition().produceType, produce);
        }
    }
}
