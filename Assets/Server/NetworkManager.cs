using RiptideNetworking;
using RiptideNetworking.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ServerSide
{
    public class NetworkManager : MonoBehaviour
    {
        public static List<ServerPlayer> players = new List<ServerPlayer>();

        private static NetworkManager instance;
        public static NetworkManager Instance
        {
            get => instance;
            private set
            {
                if (instance == null)
                    instance = value;
                else if (instance != value)
                {
                    Debug.Log($"{nameof(NetworkManager)} instance already exists, destroying object!");
                    Destroy(value);
                }
            }
        }

        public Server Server { get; private set; }
        private ushort port;
        private ushort maxClient;

        private void Awake()
        {
            Instance = this;
            port = 6112;
            maxClient = 10;
        }

        private void Start()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 30;
            Application.runInBackground = true; //Maybe dangerous

#if UNITY_EDITOR
            RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
#endif

            Server = new Server();
            Server.ClientConnected += NewPlayerConnected;
            Server.ClientDisconnected += PlayerLeft;

            Server.Start(port, maxClient);
            Debug.Log($"Server started on {port}");
        }

        private void FixedUpdate()
        {
            Server.Tick();
        }

        private void OnApplicationQuit()
        {
            Server.Stop();

            Server.ClientConnected -= NewPlayerConnected;
            Server.ClientDisconnected -= PlayerLeft;
        }

        private void NewPlayerConnected(object sender, ServerClientConnectedEventArgs e) { }

        private void PlayerLeft(object sender, ClientDisconnectedEventArgs e) { }
    }
}
