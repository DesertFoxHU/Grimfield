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

    public static void SetTileSprite(this Tilemap map, Vector3Int pos, Sprite sprite)
    {
        GrimfieldTile tile;
        if (map.HasTile(pos))
        {
            tile = map.GetTile<GrimfieldTile>(pos);
        }
        else
        {
            tile = ScriptableObject.CreateInstance<GrimfieldTile>();
        }
        tile.sprite = sprite;
        map.SetTile(pos, tile);
    }

    public static GrimfieldTile TransformToGrimfieldTile(this Tilemap map, Vector3Int pos)
    {
        if (!map.HasTile(pos))
        {
            return null;
        }

        Tile originalTile = map.GetTile<Tile>(pos);
        GrimfieldTile newTile = ScriptableObject.CreateInstance<GrimfieldTile>();

        newTile.sprite = originalTile.sprite;
        newTile.name = originalTile.name;
        newTile.flags = originalTile.flags;
        newTile.hideFlags = originalTile.hideFlags;
        newTile.colliderType = originalTile.colliderType;
        newTile.color = originalTile.color;

        map.SetTile(pos, newTile);
        return newTile;
    }

    public static string GetTileName(this Tilemap map, Vector3Int pos)
    {
        if (!map.HasTile(pos)) return null;
        return map.GetTile(pos).name;
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
