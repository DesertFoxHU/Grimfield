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

            if(Seed >= 0)
            {
                Random.InitState(Seed);
            }
            StartCoroutine(SendMapToAll());
        }

        [ContextMenu("GenerateMap")]
        private void GeneretaMap()
        {
            Tilemap map = GameObject.FindGameObjectWithTag("GameMap").GetComponent<Tilemap>();
            TileUtils utils = new(map);
            TileRegistry registry = FindObjectOfType<TileRegistry>();

            chunkManager = new ChunkManager(SizeX, SizeY);
            for(int x = 0; x <= SizeX; x++)
            {
                for (int y = 0; y <= SizeY; y++)
                {
                    float perlin = Mathf.PerlinNoise(x /10f, y /10f);
                    if (Seed >= 0) perlin = Mathf.PerlinNoise((x + Seed) /10f, (y + Seed) /10f);
                    if(perlin <= 0.20f)
                    {
                        utils.SetTileSprite(new Vector3Int(x, y, 0), registry.GetSpriteByType(TileType.Mountain));
                        chunkManager.SetTile(x, y, TileType.Mountain);
                        continue;
                    }

                    if (perlin <= 0.35f)
                    {
                        utils.SetTileSprite(new Vector3Int(x, y, 0), registry.GetSpriteByType(TileType.Forest));
                        chunkManager.SetTile(x, y, TileType.Forest);
                        continue;
                    }

                    if (Utils.Roll(3f))
                    {
                        utils.SetTileSprite(new Vector3Int(x, y, 0), registry.GetSpriteByType(TileType.GoldOre));
                        chunkManager.SetTile(x, y, TileType.GoldOre);
                        continue;
                    }

                    utils.SetTileSprite(new Vector3Int(x, y, 0), registry.GetSpriteByType(TileType.Grass));
                    chunkManager.SetTile(x, y, TileType.Grass);
                }
            }

            map.RefreshAllTiles();
            Debug.Log("Map is generated!");
        }

        private IEnumerator SendMapToAll()
        {
            yield return 0.5f;
            chunkManager.chunks.ForEach(chunk => NetworkManager.Instance.Server.SendToAll(chunk.AsPacket(MessageSendMode.reliable, (ushort)ServerToClientPacket.ChunkInfo)));
        }
    }
}