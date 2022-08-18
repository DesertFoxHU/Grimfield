using Riptide;
using Riptide.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace ServerSide
{
    public class NetworkManager : MonoBehaviour
    {
        public static List<ServerPlayer> players = new List<ServerPlayer>();

        public static ServerPlayer Find(int clientID)
        {
            return players.Find(a => a.PlayerId == clientID);
        }

        public static List<AbstractBuilding> GetAllBuilding()
        {
            List<AbstractBuilding> ab = new List<AbstractBuilding>();
            players.ForEach(x => ab.AddRange(x.Buildings));
            return ab;
        }

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
        public ushort port = 6112;
        public ushort MaxClient = 10;
        public ServerState State;
        public List<Color> PlayerColors;

        /// <summary>
        /// Only avaible if ServerState is Lobby
        /// </summary>
        public Lobby Lobby { get; set; }

        private void Awake()
        {
            Instance = this;
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
            Server.TimeoutTime = 15000;

            Message.MaxPayloadSize = 10000; //in bytes

            //FindObjectOfType<ServerConsole>().StartConsole();

            Server.Start(port, MaxClient);
            Lobby = new Lobby();
            State = ServerState.Lobby;
            Debug.Log($"Server started on {port} with {MaxClient} slots!");
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
