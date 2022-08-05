using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IProducer
{
    public ResourceType Type { get; }
    public Dictionary<int, double> ProduceLevel { get; }

    public void ProduceAtTurn(AbstractBuilding building)
    {
        if(building is IResourceStorage storage)
        {
            ResourceStorage resourceStorage = storage.Storage.Find(x => x.Type == Type);
            resourceStorage.Amount += ProduceLevel[building.Level-1];
        }
    }
}
