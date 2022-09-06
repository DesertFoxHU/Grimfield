using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class TilemapExtensions
{
    public static Vector3Int ToVector3Int(this Tilemap map, Vector3 pos)
    {
        return map.layoutGrid.WorldToCell(pos);
    }

    public static Vector3 ToVector3(this Tilemap map, Vector3Int pos)
    {
        return map.layoutGrid.CellToWorld(pos);
    }

    public static Vector3 ToVector3Center(this Tilemap map, Vector3Int pos)
    {
        Vector3 raw = map.layoutGrid.CellToWorld(pos);
        return new Vector3(raw.x + 0.5f, raw.y + 0.5f, raw.z);
    }

    public static GrimfieldTile GetOrInit(this Tilemap map, Vector3Int pos, TileDefinition definition)
    {
        if (!map.HasTile(pos))
        {
            GrimfieldTile tile = ScriptableObject.CreateInstance<GrimfieldTile>();
            tile.Init(definition);
            map.SetTile(pos, tile);
            return tile;
        }
        else return map.GetTile<GrimfieldTile>(pos);
    }

    public static void SetTileType(this Tilemap map, Vector3Int pos, TileDefinition definition)
    {
        GrimfieldTile tile;
        if (map.HasTile(pos))
        {
            tile = map.GetTile<GrimfieldTile>(pos);
            tile.definition = definition;
        }
        else
        {
            tile = ScriptableObject.CreateInstance<GrimfieldTile>();
            tile.Init(definition);
            map.SetTile(pos, tile);
        }
    }

    public static TileType? GetTileType(this Tilemap map, Vector3Int pos)
    {
        if (map.HasTile(pos))
        {
            return map.GetTile<GrimfieldTile>(pos).Type;
        }
        return null;
    }

    public static List<Vector3Int> GetNeighbour(this Tilemap map, Vector3Int pos)
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

    public static List<Vector3Int> GetTileRange(this Tilemap map, Vector3Int start, int range)
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
