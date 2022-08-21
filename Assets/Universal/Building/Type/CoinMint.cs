using ServerSide;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMint : AbstractBuilding, IDisable
{
    private bool active = true;

    public CoinMint(ServerPlayer owner, Vector3Int position) : base(owner, position) 
    {
    }

    public override BuildingType BuildingType => BuildingType.CoinMint;

    public bool IsActive()
    {
        return active;
    }

    public void SetActive(bool IsActive)
    {
        active = IsActive;
    }

    public override void OnTurnCycleEnded()
    {
        if (!IsActive()) return;

        ExchangeRate from = GetDefinition().ExchangeFrom.Find(x => x.level == Level);
        ExchangeRate to = GetDefinition().ExchangeTo.Find(x => x.level == Level);

        if (!owner.CouldStoreResource(to.type, to.amount)) return;

        //In the end they couldn't pay
        if (!owner.PayResources(KeyValuePair.Create(from.type, from.amount))) return;

        owner.TryStoreResource(to.type, to.amount);
    }
}
