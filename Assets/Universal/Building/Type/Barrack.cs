using ServerSide;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrack : AbstractBuilding
{
    public Barrack(Vector3Int position) : base(position) 
    {
    }

    public override BuildingType BuildingType => BuildingType.Barrack;

    public override void OnTurnCycleEnded(ServerPlayer owner)
    {
        
    }
}
