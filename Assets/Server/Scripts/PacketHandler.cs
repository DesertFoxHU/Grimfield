using RiptideNetworking;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ServerSide
{
    [RequireComponent(typeof(GameController))]
    public class PacketHandler : MonoBehaviour
    {
        public static Tilemap map;
        private static GameController gameController;

        public void Start()
        {
            PacketHandler.map = GameObject.FindGameObjectWithTag("GameMap").GetComponent<Tilemap>();
            gameController = this.GetComponent<GameController>();
        }

        [MessageHandler((ushort)ClientToServerPacket.JoinLobby)]
        private static void JoinLobby(ushort clientID, Message message)
        {
            string name = message.GetString();

            if(name == null)
            {
                NetworkManager.Instance.Server.Send(
                    Message.Create(MessageSendMode.reliable,
                    ServerToClientPacket.SendAlert).AddString("Server: Your name is empty"),
                    clientID);
                NetworkManager.Instance.Server.DisconnectClient(clientID);
                return;
            }

            if (name.Length > 16)
            {
                NetworkManager.Instance.Server.Send(
                    Message.Create(MessageSendMode.reliable,
                    ServerToClientPacket.SendAlert).AddString("Server: Your name cannot be longer than 16 characters!"),
                    clientID);
                NetworkManager.Instance.Server.DisconnectClient(clientID);
                return;
            }

            Debug.Log($"{name} joined to the lobby!");
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
            gameController.SendMapTo(clientID);

            bool everyoneReady = true;
            foreach (ServerPlayer sPlayer in NetworkManager.players)
            {
                if (!sPlayer.IsMainSceneLoaded)
                {
                    everyoneReady = false;
                    break;
                }
            }

            if(everyoneReady) ServerSender.TurnChange(gameController.turnHandler.turnOrder[gameController.turnHandler.currentIndex], gameController.turnHandler.turnCycleCount);
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

        [MessageHandler((ushort)ClientToServerPacket.NextTurn)]
        private static void NextTurn(ushort clientID, Message message)
        {
            if(gameController == null)
            {
                Debug.LogError("GameController is not initialized!");
                return;
            }

            if(gameController.turnHandler == null)
            {
                Debug.LogWarning("TurnHandler is not initialized!");
                return;
            }

            if(gameController.turnHandler.GetCurrentTurnOwnerID() != clientID)
            {
                NetworkManager.Instance.Server.Send(
                    Message.Create(MessageSendMode.unreliable, ServerToClientPacket.SendAlert).Add("Stop trolling! Its not your turn yet!"),
                    clientID);
                return;
            }

            gameController.turnHandler.TurnEnded();
        }
    }
}