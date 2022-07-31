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

        public ServerPlayer(ushort playerId, string name)
        {
            PlayerId = playerId;
            Name = name;
            NetworkManager.players.Add(this);
        }
    }
}
