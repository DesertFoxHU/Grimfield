using System;
using System.Collections.Generic;
using UnityEngine;

namespace ServerSide
{
    public class ServerTerritory : Territory
    {
        public AbstractBuilding building = null;
        public Vector3Int Position;
        public Color Color { get; private set; }

        public ServerTerritory(AbstractBuilding building, ushort clientID, List<Vector3Int> claimedLand) : base(clientID, claimedLand)
        {
            this.building = building;
            Position = building.Position;
            Color = NetworkManager.Find(clientID).Color;
        }

        public ServerTerritory(Vector3Int Position, ushort clientID, Color color, List<Vector3Int> claimedLand) : base(clientID, claimedLand)
        {
            this.Position = Position;
            Color = color;
        }

        public override Color GetColor() => Color;
    }
}