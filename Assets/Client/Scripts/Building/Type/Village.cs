using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : AbstractBuilding, IProducer, IResourceStorage
{
    public Village(Vector3Int position) : base(position) { }

    public override BuildingType BuildingType => BuildingType.Village;

    public ResourceType Type => ResourceType.Citizen;

    public Dictionary<int, double> ProduceLevel => new Dictionary<int, double>()
    {
        { 1, 0.25 },
        { 2, 0.5 },
        { 3, 1 },
        { 4, 1.5 },
        { 5, 2 }
    };

    public List<ResourceStorage> Storage => new List<ResourceStorage>() 
    { 
        new ResourceStorage(ResourceType.Citizen, new Dictionary<int, double>() 
        {
            { 1, 4d }
        })
    };
}
