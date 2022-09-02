using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

namespace ServerSide
{
    public class TerritoryRenderer : Universal.TerritoryRenderer
    {
        /// <summary>
        /// Only server side
        /// </summary>
        public void TryAddNew(Tilemap map, ServerPlayer owner, AbstractBuilding building)
        {
            if (!building.GetDefinition().canClaimTerritory) return;

            building.OnClaimLand(map);
            ServerTerritory territory = new ServerTerritory(building, building.owner.PlayerId, building.ClaimedLand);
            territory.ID = GetNextTerritoryId();

            territories.Add(territory);
            building.territory = territory;

            RenderAll();
            ServerSender.RenderTerritory(owner, building);
        }

        /// <summary>
        /// Calculates a new Unique ID (incremental) for a territory
        /// </summary>
        /// <returns>Returns the first non exist ID</returns>
        public int GetNextTerritoryId()
        {
            int Id = 0;
            foreach (ServerTerritory territory in territories)
            {
                if (territory.ID == Id)
                {
                    Id++;
                }
            }

            return Id;
        }
    }
}