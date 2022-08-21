using System;
using System.Collections.Generic;
using UnityEngine;

namespace ServerSide
{
    public class ServerTerritory : Territory
    {
        public AbstractBuilding building;
        public Color Color { get; private set; }

        public ServerTerritory(AbstractBuilding building, ushort clientID, List<Vector3Int> claimedLand) : base(clientID, claimedLand)
        {
            this.building = building;
            Color = NetworkManager.Find(clientID).Color;
        }

        public override Color GetColor() => Color;
    }
}