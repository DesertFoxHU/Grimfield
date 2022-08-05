using RiptideNetworking;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

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
            res.UpdateType(type, amount, 0);
        }
    }
}
