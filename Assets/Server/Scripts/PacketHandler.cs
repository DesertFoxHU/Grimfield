using RiptideNetworking;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ServerSide
{
    public class PacketHandler : MonoBehaviour
    {
        public static Tilemap map;

        public void Start()
        {
            PacketHandler.map = GameObject.FindGameObjectWithTag("GameMap").GetComponent<Tilemap>();
        }

        [MessageHandler((ushort)ClientToServerPacket.JoinLobby)]
        private static void JoinLobby(ushort clientID, Message message)
        {
            string name = message.GetString();

            if(name == null)
            {
                FindObjectOfType<NetworkManager>().Server.Send(
                    Message.Create(MessageSendMode.reliable,
                    ServerToClientPacket.SendAlert).AddString("Server: Your name is empty"),
                    clientID);
                return;
            }

            Debug.Log($"Somebody joined the lobby as {name}");
            new ServerPlayer(clientID, name);

            Message lobbyMsg = Message.Create(MessageSendMode.reliable, ServerToClientPacket.LoadLobby);
            FindObjectOfType<NetworkManager>().Server.Send(lobbyMsg, clientID);
        }

        [MessageHandler((ushort)ClientToServerPacket.ChangeReadyStatus)]
        private static void ChangeReadyStatus(ushort clientID, Message message)
        {
            ServerPlayer player = NetworkManager.Find(clientID);
            if(player == null)
            {
                Debug.LogError($"Unathorized ready status change from: {clientID}");
                return;
            }

            player.IsReady = message.GetBool();

            bool isEveryoneRead = true;
            foreach(ServerPlayer serverPlayer in NetworkManager.players)
            {
                if (!serverPlayer.IsReady)
                {
                    isEveryoneRead = false;
                    break;
                }
            }

            if (isEveryoneRead)
            {
                FindObjectOfType<GameController>().StartMatchGame();
            }
        }

        [MessageHandler((ushort)ClientToServerPacket.MainGameLoaded)]
        private static void OnMainGameLoaded(ushort clientID, Message message)
        {
            ServerPlayer player = NetworkManager.Find(clientID);
            player.IsMainSceneLoaded = true;
            FindObjectOfType<GameController>().SendMapTo(clientID);
        }

        [MessageHandler((ushort)ClientToServerPacket.RequestBuild)]
        private static void RequestToBuild(ushort clientID, Message message)
        {
            Vector3 v3Pos = message.GetVector3();
            Vector3Int pos = GameObject.FindGameObjectWithTag("GameMap").GetComponent<Tilemap>().ToVector3Int(v3Pos);
            BuildingType type = (BuildingType) Enum.Parse(typeof(BuildingType), message.GetString());

            TileDefiniton definition = DefinitionRegistry.Instance.Find(map.GetTileName(pos));
            if (definition == null)
            {
                Debug.LogError("Definition for this position wasn't loaded!");
                return;
            }

            BuildingDefinition buildingDefinition = DefinitionRegistry.Instance.Find(type);
            if (!buildingDefinition.placeable.Contains(definition.type))
            {
                NetworkManager.Instance.Server.Send(
                    Message.Create(MessageSendMode.unreliable, ServerToClientPacket.SendAlert).Add("You can't place this building here"),
                    clientID);
                return;
            }

            ServerPlayer player = NetworkManager.Find(clientID);
            AbstractBuilding building = (AbstractBuilding) Activator.CreateInstance(AbstractBuilding.GetClass(type), pos);

            player.Buildings.Add(building);
            player.IncrementBuildingBought(type);
            ServerSender.SendNewBuilding(player, building);
            Debug.Log($"Added new building with GUID {building.ID}");
        }
    }
}