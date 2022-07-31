using RiptideNetworking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ServerSide
{
    public class LobbyUpdater : MonoBehaviour
    {
        private float timer = 1f;
        void Update()
        {
            timer -= Time.deltaTime;
            if(timer <= 0f)
            {
                CallEverySec();
                timer = 1f;
            }
        }

        private void CallEverySec()
        {
            if (NetworkManager.Instance.State != ServerState.Lobby) return;

            Message message = Message.Create(MessageSendMode.unreliable, ServerToClientPacket.UpdateLobby);
            message.Add(NetworkManager.players.Count);
            message.Add(NetworkManager.Instance.MaxClient);
            foreach (ServerPlayer player in NetworkManager.players)
            {
                message.Add(player.Name + "|" + player.IsReady);
            }
            NetworkManager.Instance.Server.SendToAll(message);
        }
    }
}