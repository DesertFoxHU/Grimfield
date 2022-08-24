using InfoPanel;
using Riptide;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using Universal;

public class PacketHandler : MonoBehaviour
{
    private static Tilemap map;
    
    private static void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
    {
        if(scene.name == "MainGame")
        {
            map = GameObject.FindGameObjectWithTag("GameMap").GetComponent<Tilemap>();
            if(map == null)
            {
                Debug.LogError("Couldn't grab reference of GameMap TileMap!");
            }

            Message message = Message.Create(MessageSendMode.reliable, ClientToServerPacket.MainGameLoaded);
            NetworkManager.Instance.Client.Send(message);
        }
    }

    [MessageHandler((ushort)ServerToClientPacket.SendAlert)]
    private static void OnAlertRecieve(Message message)
    {
        string msg = message.GetString();
        FindObjectOfType<MessageDisplayer>().SetMessage(msg);
    }

    [MessageHandler((ushort)ServerToClientPacket.LoadLobby)]
    private static void LobbyLoad(Message message)
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("LobbyScene", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(2);
    }

    [MessageHandler((ushort)ServerToClientPacket.UpdateLobby)]
    private static void LobbyUpdate(Message message)
    {
        int count = message.GetInt();
        ushort maxPlayer = message.GetUShort();

        foreach(GameObject go in GameObject.FindGameObjectsWithTag("LobbyPlayersText")){
            go.GetComponent<TMPro.TextMeshProUGUI>().text = $"Players: {count}/{maxPlayer}";
        }

        List<string> raw = new List<string>();
        for(int i = 0; i < count; i++)
        {
            raw.Add(message.GetString());
        }

        LobbyPlayerList playerList = FindObjectOfType<LobbyPlayerList>();
        if (playerList != null) playerList.UpdateList(raw);
    }

    [MessageHandler((ushort)ServerToClientPacket.LoadGameScene)]
    private static void MainGameLoad(Message message)
    {
        SceneManager.LoadScene("MainGame", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("LobbyScene");
    }

    [MessageHandler((ushort)ServerToClientPacket.ChunkInfo)]
    private static void ChunkUpdate(Message message)
    {
        //ChunkLoader.LoadChunk(map, message);
        int chunkX = message.GetInt();
        int chunkY = message.GetInt();
        int listCount = message.GetInt();

        for (int i = 0; i < listCount; i++)
        {
            string raw = message.GetString();
            string[] split = raw.Split('|');
            Vector3Int pos = new Vector3Int(int.Parse(split[0]), int.Parse(split[1]), 0);
            TileType tileType = (TileType)Enum.Parse(typeof(TileType), split[2]);
            int spriteIndex = int.Parse(split[3]);

            map.SetTileSprite(pos, DefinitionRegistry.Instance.Find(tileType).sprites[spriteIndex]);
        }
        map.RefreshAllTiles();
    }

    [MessageHandler((ushort)ServerToClientPacket.NewBuildingAdded)]
    private static void NewBuildingSpawn(Message message)
    {
        ushort clientID = message.GetUShort();
        Guid ID = message.GetGuid();
        BuildingType type = (BuildingType)Enum.Parse(typeof(BuildingType), message.GetString());
        Vector3Int pos = message.GetVector3Int();
        int level = message.GetInt();

        GameObject go = new GameObject();
        go.name = "Building_" + clientID + "_" + ID.ToString();
        go.transform.position = new Vector3(pos.x + .5f, pos.y + .5f, -1f);
        go.transform.SetParent(map.transform);
        SpriteRenderer render = go.AddComponent<SpriteRenderer>();

        render.sprite = DefinitionRegistry.Instance.Find(type).GetSpriteByLevel(level);
    }

    [MessageHandler((ushort)ServerToClientPacket.PlayerResourceUpdate)]
    private static void UpdateResources(Message message)
    {
        ResourceText res = FindObjectOfType<ResourceText>();
        if (res == null)
        {
            Debug.LogWarning("Can't get reference for ResourceText!");
            return;
        }

        int readIn = message.GetInt();
        for(int i = 0; i < readIn; i++)
        {
            ResourceType type = (ResourceType)System.Enum.Parse(typeof(ResourceType), message.GetString());
            double amount = message.GetDouble();
            double perTurn = message.GetDouble();
            res.UpdateType(type, amount, perTurn);
            PlayerInfo.UpdateResource(type, amount);
        }
    }

    [MessageHandler((ushort)ServerToClientPacket.SyncPlayers)]
    private static void SyncPlayers(Message message)
    {
        int playerCount = message.GetInt();
        NetworkManager.Instance.ClientPlayer = new ClientPlayer(message.GetUShort(), message.GetString(), message.GetColor());
        NetworkManager.Instance.Players = new List<ClientPlayer>();
        for (int i = 0; i < message.GetInt(); i++)
        {
            NetworkManager.Instance.Players.Add(new ClientPlayer(message.GetUShort(), message.GetString(), message.GetColor()));
        }
    }

    [MessageHandler((ushort)ServerToClientPacket.TurnChange)]
    private static void OnTurnChange(Message message)
    {
        ushort CurrentID = message.GetUShort();
        int turnCycle = message.GetInt();
        if(CurrentID == NetworkManager.Instance.ClientPlayer.ClientID)
        {
            NetworkManager.Instance.IsYourTurn = true;
        }
        else NetworkManager.Instance.IsYourTurn = true;

        ClientPlayer currentPlayer = NetworkManager.Instance.GetAllPlayer().Find(x => x.ClientID == CurrentID);
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("TurnInfo"))
        {
            TextMeshProUGUI gui = go.GetComponent<TextMeshProUGUI>();
            gui.text = $"Turn({turnCycle}): {currentPlayer.Name}";
            gui.color = currentPlayer.Color;
        }
    }

    [MessageHandler((ushort)ServerToClientPacket.UpdateBuildingCost)]
    private static void UpdateBuildingCost(Message message)
    {
        BuildingType type = (BuildingType)Enum.Parse(typeof(BuildingType), message.GetString());
        int boughtAmount = message.GetInt();

        BuildPanel panel = FindObjectOfType<BuildPanel>();
        if (panel.BuildingBought.ContainsKey(type))
        {
            panel.BuildingBought[type] = boughtAmount;
        }
        else panel.BuildingBought.Add(type, boughtAmount);

        panel.GetSegment(type).RenderCost(boughtAmount);
    }

    [MessageHandler((ushort)ServerToClientPacket.FetchBuildingDataResponse)]
    private static void FetchBuildingDataResponse(Message message)
    {
        AbstractBuilding building = message.GetBuilding();
        FindObjectOfType<InfoWindow>().Load(building);
    }

    [MessageHandler((ushort)ServerToClientPacket.RenderTerritory)]
    private static void RenderTerritory(Message message)
    {
        ushort clientID = message.GetUShort();
        int count = message.GetInt();
        List<Vector3Int> claimed = new List<Vector3Int>(); 
        for(int i = 0; i < count; i++)
        {
            string raw = StringCompressor.DecompressString(message.GetString());
            Vector3Int v3 = new Vector3Int(int.Parse(raw.Split('|')[0]), int.Parse(raw.Split('|')[1]), 0);
            claimed.Add(v3);
        }
        TerritoryRenderer.Instance.territories.Add(new ClientTerritory(clientID, claimed));
        TerritoryRenderer.Instance.RenderAll();
    }

    [MessageHandler((ushort)ServerToClientPacket.SpawnEntity)]
    private static void SpawnEntity(Message message)
    {
        ushort clientID = message.GetUShort();
        EntityType type = (EntityType) Enum.Parse(typeof(EntityType), message.GetString());
        Vector3Int position = message.GetVector3Int();

        Vector3 pos = map.ToVector3(position);
        pos = new Vector3(pos.x + 0.5f, pos.y + 0.5f, -1.1f);

        EntityDefinition definition = FindObjectOfType<DefinitionRegistry>().Find(type);
        GameObject go = Instantiate(definition.Prefab, pos, Quaternion.identity);
        go.GetComponent<Entity>().Initialize(definition);
    }
}
