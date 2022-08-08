using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orchard : AbstractBuilding, IProducer
{
    public Orchard(Vector3Int position) : base(position) 
    {
    }

    public override BuildingType BuildingType => BuildingType.Orchard;

    public ResourceType Type => ResourceType.Food;

    public Dictionary<int, double> ProduceLevel => new Dictionary<int, double>()
    {
        { 1, 0.1 },
        { 2, 0.25 },
        { 3, 0.5 },
        { 4, 1.0 },
        { 5, 2 }
    };

    public override void OnTurnCycleEnded()
    {
        //Storage[0].AddSafe(ProduceLevel[this.Level]);
    }
}
