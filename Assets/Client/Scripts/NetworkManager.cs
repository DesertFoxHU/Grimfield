using RiptideNetworking;
using RiptideNetworking.Utils;
using System.Collections;
using System.Collections.Generic;
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

    private void Awake() { Instance = this; }

    private void Start()
    {
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, true);

        Client = new Client();
    }

    private void FixedUpdate()
    {
        Client.Tick();
    }

    private void OnApplicationQuit()
    {
        Client.Disconnect();
    }

    /*public void Connect()
    {
        Client.Connect();
    }*/


}
