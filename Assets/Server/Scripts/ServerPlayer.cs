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

        public Dictionary<ResourceType, double> GetResourceGeneratePerTurn()
        {
            Dictionary<ResourceType, double> generate = new Dictionary<ResourceType, double>();
            foreach (AbstractBuilding building in Buildings)
            {
                if (building is IProducer producer)
                {
                    ResourceType type = producer.Type;
                    if (generate.ContainsKey(type))
                    {
                        generate[type] += producer.ProduceLevel[building.Level];
                    }
                    else
                    {
                        generate.Add(type, producer.ProduceLevel[building.Level]);
                    }
                }
            }
            return generate;
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

        /// <summary>
        /// This always check if the player has enough resource to pay it
        /// </summary>
        /// <param name="cost"></param>
        /// <returns>False if couldn't pay</returns>
        public bool PayResources(Dictionary<ResourceType, double> cost)
        {
            #region Has enough 
            List<ResourceHolder> resources = GetAvaibleResources();
            foreach (ResourceType resType in cost.Keys)
            {
                ResourceHolder holder = resources.Get(resType);
                if (holder == null || holder.Value < cost[resType])
                {
                    return false;
                }
            }
            #endregion

            foreach (ResourceType resType in cost.Keys)
            {
                double needToPay = cost[resType];
                foreach (AbstractBuilding building in Buildings)
                {
                    if (needToPay <= 0) break;

                    if(building is IResourceStorage storage)
                    {
                        ResourceStorage actualStorage = storage.Storage.Get(resType);
                        if (actualStorage == null || actualStorage.Amount <= 0) continue;

                        //If we have less stored than we need
                        //we take all of it from the building
                        if(needToPay >= actualStorage.Amount)
                        {
                            needToPay -= actualStorage.Amount;
                            actualStorage.Amount = 0;
                        }
                        else //We need to play less than we have
                        {
                            actualStorage.Amount -= needToPay;
                            needToPay = 0;
                        }
                    }
                }

                //None of the buildings could pay for it :(
                if(needToPay > 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
