using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Obsolete("Use the added extensions to Tilemap instead. See: TilemapExtensions.cs", true)]
public class TileUtils
{
    private readonly Tilemap map;
    private readonly Grid grid;

    public TileUtils(Tilemap map)
    {
        this.map = map;
        this.grid = map.layoutGrid;
    }

    public TileUtils(GameObject mapObject)
    {
        this.map = mapObject.GetComponent<Tilemap>();
        this.grid = map.layoutGrid;
    }

    public Vector3Int ToVector3Int(Vector3 pos)
    {
        return grid.WorldToCell(pos);
    }

    public Vector3 ToVector3(Vector3Int pos)
    {
        return grid.CellToWorld(pos);
    }

    public void SetTileSprite(Vector3Int pos, Sprite sprite)
    {
        Tile tile = ScriptableObject.CreateInstance<Tile>();
        tile.sprite = sprite;
        map.SetTile(pos, tile);
        //map.RefreshTile(pos);
    }

    /// <summary>
    /// If the position doesn't have any tile it will return a null
    /// </summary>
    /// <param name="pos"></param>
    /// <returns>null if map doesn't have this tile</returns>
    public string GetTileName(Vector3Int pos)
    {
        if (!map.HasTile(pos)) return null;
        return map.GetTile(pos).name;
    }

    public List<Vector3Int> GetNeighbour(Vector3Int pos)
    {
        List<Vector3Int> neigh = new List<Vector3Int>();

        Vector3Int up = new Vector3Int(pos.x, pos.y + 1, pos.z);
        Vector3Int down = new Vector3Int(pos.x, pos.y - 1, pos.z);
        Vector3Int right = new Vector3Int(pos.x + 1, pos.y, pos.z);
        Vector3Int left = new Vector3Int(pos.x - 1, pos.y, pos.z);

        if (map.HasTile(up)) neigh.Add(up);
        if (map.HasTile(down)) neigh.Add(down);
        if (map.HasTile(right)) neigh.Add(right);
        if (map.HasTile(left)) neigh.Add(left);

        return neigh;
    }

    public List<Vector3Int> GetTileRange(Vector3Int start, int range)
    {
        List<Vector3Int> list = new List<Vector3Int>();
        for (int x = start.x - range; x <= start.x + range; x++)
        {
            for (int y = start.y - range; y <= start.y + range; y++)
            {
                Vector3Int asV = new Vector3Int(x, y, 0);
                if (!map.HasTile(asV))
                {
                    continue;
                }

                list.Add(asV);
            }
        }
        return list;
    }
}
