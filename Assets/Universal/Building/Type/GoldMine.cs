using ServerSide;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMine : AbstractBuilding, IProducer
{
    public GoldMine(Vector3Int position) : base(position) 
    {
    }

    public override BuildingType BuildingType => BuildingType.GoldMine;

    public ResourceType Type => ResourceType.Gold;

    public Dictionary<int, double> ProduceLevel => new Dictionary<int, double>()
    {
        { 1, 0.1 },
        { 2, 0.25 },
        { 3, 0.5 },
        { 4, 1.0 },
        { 5, 2 }
    };

    public override void OnTurnCycleEnded(ServerPlayer owner)
    {
        owner.TryStoreResource(Type, ProduceLevel[this.Level]);
    }
}
