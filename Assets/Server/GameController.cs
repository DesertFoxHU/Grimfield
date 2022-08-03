using RiptideNetworking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ServerSide
{
    public class GameController : MonoBehaviour
    {
        [HideInInspector] public ChunkManager chunkManager;
        public int Seed = -1;
        public int SizeX;
        public int SizeY;

        public void StartMatchGame()
        {
            NetworkManager.Instance.State = ServerState.Playing;
            GeneretaMap();

            Message startGamePacket = Message.Create(MessageSendMode.reliable, ServerToClientPacket.LoadGameScene);
            NetworkManager.Instance.Server.SendToAll(startGamePacket);
        }

        [ContextMenu("GenerateMap")]
        private void GeneretaMap()
        {
            if (Seed == -1) Seed = (int)System.DateTime.Now.Ticks;
            Random.InitState(Seed);

            Tilemap map = GameObject.FindGameObjectWithTag("GameMap").GetComponent<Tilemap>();

            chunkManager = new ChunkManager(SizeX, SizeY);
            for(int x = 0; x <= SizeX; x++)
            {
                for (int y = 0; y <= SizeY; y++)
                {
                    float perlin = Mathf.PerlinNoise((x + Seed/100f) /10f, (y + Seed/100f) /10f);
                    if(perlin <= 0.20f)
                    {
                        GenerateTile(map, x, y, TileType.Mountain);
                        continue;
                    }

                    if (perlin <= 0.25f && Utils.Roll(7f))
                    {
                        GenerateTile(map, x, y, TileType.GoldOre);
                        continue;
                    }

                    if (perlin <= 0.35f)
                    {
                        GenerateTile(map, x, y, TileType.Forest);
                        continue;
                    }

                    GenerateTile(map, x, y, TileType.Grass);
                }
            }

            map.RefreshAllTiles();
            Debug.Log("Map is generated!");
        }

        private void GenerateTile(Tilemap map, int x, int y, TileType type)
        {
            TileDefiniton definition = DefinitionRegistry.Instance.Find(type);
            int spriteIndex = definition.GetRandomSpriteIndex();
            map.SetTileSprite(new Vector3Int(x, y, 0), definition.GetRandomSprite());
            chunkManager.SetTile(x, y, type, spriteIndex);
        }

        public void SendMapToAll()
        {
            chunkManager.chunks.ForEach(chunk => NetworkManager.Instance.Server.SendToAll(chunk.AsPacket(MessageSendMode.reliable, (ushort)ServerToClientPacket.ChunkInfo)));
        }

        public void SendMapTo(ushort clientID)
        {
            chunkManager.chunks.ForEach(chunk => 
            NetworkManager.Instance.Server.Send(chunk.AsPacket(
                MessageSendMode.reliable, 
                (ushort)ServerToClientPacket.ChunkInfo), clientID));
        }
    }
}