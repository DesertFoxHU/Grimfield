using Riptide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ServerSide
{
    public class EntityManager
    {
        public static bool OnMoveTo(ServerPlayer player, Tilemap map, Entity entity, Vector3Int newPosition)
        {
            //TODO: Enemy entity attack
            //TODO: Claim enemy building
            //TODO: Attack enemy building
            return true;
        }

        public static void DestroyEntity(Entity entity)
        {
            ServerSender.DestroyEntity(entity);
            UnityEngine.Object.Destroy(entity.gameObject);
        }

        /// <summary>
        /// Calculates a new Unique ID (incremental) for an entity
        /// </summary>
        /// <returns>Returns the first non exist ID</returns>
        public static int GetNextEntityId()
        {
            int Id = 0;
            foreach(Entity entity in UnityEngine.Object.FindObjectsOfType<Entity>())
            {
                if(entity.Id == Id)
                {
                    Id++;
                }
            }

            return Id;
        }
    }
}
