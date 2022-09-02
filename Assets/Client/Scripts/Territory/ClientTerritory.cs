using System;
using System.Collections.Generic;
using UnityEngine;

public class ClientTerritory : Territory
{
    public Color Color { get; private set; }

    public ClientTerritory(ushort clientID, int ID, int index, List<Vector3Int> claimedLand)
    {
        ClientID = clientID;
        this.ID = ID;
        Color = NetworkManager.Instance.GetAllPlayer().Find(x => x.ClientID == ClientID).Color;
        LoadPartly(index, claimedLand);
    }

    public void LoadPartly(int index, List<Vector3Int> claimedLand)
    {
        if (!ClaimedLand.ContainsKey(index))
        {
            ClaimedLand.Add(index, claimedLand);
        }
        else
        {
            ClaimedLand[index] = claimedLand;
        }
    }

    public override Color GetColor() => Color;
}
