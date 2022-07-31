using RiptideNetworking;
using RiptideNetworking.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    private static NetworkManager instance;
    public static NetworkManager Instance
    {
        get => instance;
        set
        {
            if(instance == null)
            {
                instance = value;
            }
            else
            {
                Debug.LogError("NetworkManager instance is already exist!");
                Destroy(value);
            }
        }
    }

    public Client Client { get; private set; }
    public string Name { get; set; }

    private void Awake() { Instance = this; }

    private void Start()
    {
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, true);

        Client = new Client();
        Client.Connected += DidConnect;
        Debug.Log("Created new RiptideClient!");
    }

    private void FixedUpdate()
    {
        Client.Tick();
    }

    private void OnApplicationQuit()
    {
        Client.Disconnect();
    }

    public void Connect()
    {
        Client.Connect($"192.168.1.180:6112");
        Debug.Log($"Attempt to join default server");
    }

    public void DidConnect(object sender, EventArgs e)
    {
        Message message = Message.Create(MessageSendMode.reliable, ClientToServerPacket.JoinLobby);
        message.Add(Name);
        Client.Send(message);
    }

}
