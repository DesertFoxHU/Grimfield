using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ServerSide
{
    public class EntityLogic
    {
        public static bool OnMoveTo(ServerPlayer player, Tilemap map, Entity entity, Vector3Int newPosition)
        {
            //TODO: Enemy entity attack
            //TODO: Claim enemy building
            //TODO: Attack enemy building
            return true;
        }
    }
}
