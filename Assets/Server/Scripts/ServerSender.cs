using RiptideNetworking;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ServerSide
{
    public static class ServerSender
    {
        public static void SendAlert(ushort clientID, string message)
        {
            NetworkManager.Instance.Server.Send(
                    Message.Create(MessageSendMode.unreliable, ServerToClientPacket.SendAlert).Add(message),
                    clientID);
        }

        /// <summary>
        /// Sends everybody a request to render the new building
        /// </summary>
        /// <param name="building"></param>
        public static void SendNewBuilding(ServerPlayer owner, AbstractBuilding building)
        {
            Message message = Message.Create(MessageSendMode.reliable, ServerToClientPacket.NewBuildingAdded);
            message.Add(owner.PlayerId);
            message.Add(building.ID);
            message.Add(building.BuildingType.ToString());
            message.Add(building.Position);
            message.Add(building.Level);

            NetworkManager.Instance.Server.SendToAll(message);
        }

        public static void UpdatePlayerResource(ServerPlayer player, List<ResourceHolder> summerized, Dictionary<ResourceType, double> perTurn)
        {
            Message message = Message.Create(MessageSendMode.unreliable, ServerToClientPacket.PlayerResourceUpdate);
            message.Add(summerized.Count);
            foreach (ResourceHolder holder in summerized)
                message.Add(holder.type.ToString()).Add(holder.Value).Add(perTurn.ContainsKey(holder.type) ? perTurn[holder.type] : 0);

            NetworkManager.Instance.Server.Send(message, player.PlayerId);
        }

        public static void SyncPlayers()
        {
            foreach(ServerPlayer player in NetworkManager.players)
            {
                //The first half of the message is this player's ServerPlayer info
                Message message = Message.Create(MessageSendMode.reliable, ServerToClientPacket.SyncPlayers);
                message.Add(NetworkManager.players.Count);
                message.Add(player.PlayerId);
                message.Add(player.Name);
                message.Add(NetworkManager.players.Count - 1); //-1 because we already added the owner's ServerPlayer
                foreach (ServerPlayer otherPlayer in NetworkManager.players)
                    if (otherPlayer.PlayerId != player.PlayerId) message.Add(otherPlayer.PlayerId).Add(otherPlayer.Name);

                NetworkManager.Instance.Server.Send(message, player.PlayerId);
            }
        }

        public static void TurnChange(ServerPlayer currentPlayer, int turnCycle)
        {
            Message message = Message.Create(MessageSendMode.reliable, ServerToClientPacket.TurnChange);
            message.Add(currentPlayer.PlayerId);
            message.Add(turnCycle);
            NetworkManager.Instance.Server.SendToAll(message);
        }
    }
}