using System;
using System.Collections.Generic;
using UnityEngine;

public class ClientTerritory : Territory
{
    public Color Color { get; private set; }

    public ClientTerritory(ushort clientID, List<Vector3Int> claimedLand) : base(clientID, claimedLand)
    {
        Color = NetworkManager.Instance.GetAllPlayer().Find(x => x.ClientID == ClientID).Color;
    }

    public override Color GetColor() => Color;
}
