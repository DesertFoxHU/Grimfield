using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ServerSide
{
    public class ServerPlayer
    {
        public ushort PlayerId { get; private set; }
        public string Name { get; private set; }
        public bool IsReady = false;
        public bool IsMainSceneLoaded = false;

        public List<AbstractBuilding> Buildings { get; private set; } = new List<AbstractBuilding>();
        //How many times a building bought by the player
        public Dictionary<BuildingType, int> BuildingBought { get; private set; } = new();

        public ServerPlayer(ushort playerId, string name)
        {
            PlayerId = playerId;
            Name = name;
            NetworkManager.players.Add(this);
        }

        public List<ResourceHolder> GetAvaibleResources()
        {
            List<ResourceHolder> resources = new List<ResourceHolder>();
            foreach (AbstractBuilding building in Buildings)
            {
                if(building is IResourceStorage storage)
                {
                    foreach (ResourceStorage res in storage.Storage)
                    {
                        ResourceHolder holder = resources.GetOrCreate(res.Type);
                        holder.Value += res.Amount;
                    }
                }
            }
            return resources;
        }

        public void IncrementBuildingBought(BuildingType type)
        {
            if (BuildingBought.ContainsKey(type))
            {
                BuildingBought[type] += 1;
            }
            else BuildingBought.Add(type, 1);
        }

        public AbstractBuilding GetBuildingByID(Guid ID)
        {
            return Buildings.Find(x => x.ID == ID);
        }
    }
}
