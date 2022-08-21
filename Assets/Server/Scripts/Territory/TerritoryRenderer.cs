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
        public void TryAddNew(Tilemap map, AbstractBuilding building)
        {
            if (!building.GetDefinition().canClaimTerritory) return;

            building.OnClaimLand(map);
            ServerTerritory territory = new ServerTerritory(building, building.owner.PlayerId, building.ClaimedLand);
            territories.Add(territory);

            RenderAll();
        }
    }
}