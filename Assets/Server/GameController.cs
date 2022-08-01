using RiptideNetworking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ServerSide
{
    public class GameController : MonoBehaviour
    {
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
            Tilemap map = GameObject.FindGameObjectWithTag("GameMap").GetComponent<Tilemap>();
            TileUtils utils = new(map);
            TileRegistry registry = FindObjectOfType<TileRegistry>();

            for(int x = 0; x <= 30; x++)
            {
                for (int y = 0; y <= 30; y++)
                {
                    if(Random.Range(0f, 100f) <= 10f)
                    {
                        utils.SetTileSprite(new Vector3Int(x, y, 0), registry.GetSpriteByType(TileType.Mountain));
                        continue;
                    }

                    if (Random.Range(0f, 100f) <= 5f)
                    {
                        utils.SetTileSprite(new Vector3Int(x, y, 0), registry.GetSpriteByType(TileType.GoldOre));
                        continue;
                    }

                    if (Random.Range(0f, 100f) <= 15f)
                    {
                        utils.SetTileSprite(new Vector3Int(x, y, 0), registry.GetSpriteByType(TileType.Forest));
                        continue;
                    }

                    utils.SetTileSprite(new Vector3Int(x, y, 0), registry.GetSpriteByType(TileType.Grass));
                }
            }

            map.RefreshAllTiles();
            Debug.Log("Map is generated!");
        }
    }
}