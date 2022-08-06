using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : AbstractBuilding, IProducer, IResourceStorage
{
    public Village(Vector3Int position) : base(position) 
    {
        BuildingStorage = new List<ResourceStorage>
        {
            new ResourceStorage(this, ResourceType.Citizen, new Dictionary<int, double>()
            {
                { 1, 4d }
            })
        };
    }

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

    public override void OnTurnCycleEnded()
    {
        Storage[0].AddSafe(ProduceLevel[this.Level]);
    }
}
