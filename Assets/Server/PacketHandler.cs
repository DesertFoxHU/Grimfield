using RiptideNetworking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketHandler : MonoBehaviour
{
    [MessageHandler((ushort)ClientToServerPacket.JoinLobby)]
    private static void JoinLobby(ushort clientID, Message message)
    {
        string name = message.GetString();
        Debug.Log($"Somebody joined the lobby as {name}");
    }
}
