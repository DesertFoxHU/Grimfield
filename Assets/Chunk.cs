using RiptideNetworking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
    public readonly int ChunkX;
    public readonly int ChunkY;
    public Dictionary<Vector3Int, TileType> Tiles = new Dictionary<Vector3Int, TileType>();

    public Chunk(int chunkX, int chunkY)
    {
        ChunkX = chunkX;
        ChunkY = chunkY;
    }

    public Message AsPacket(MessageSendMode mode, ushort packetId)
    {
        Message message = Message.Create(mode, packetId);
        message.Add(ChunkX);
        message.Add(ChunkY);
        message.Add(Tiles.Keys.Count);
        foreach(Vector3Int v3 in Tiles.Keys)
        {
            TileType val = Tiles[v3];
            message.Add(v3.x + "|" + v3.y + "|" + val);
        }
        return message;
    }
}
