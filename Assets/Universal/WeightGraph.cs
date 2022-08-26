using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Collections;
using System.Collections.Generic;

public class WeightGraph
{
    public Tilemap map;
    public Vector3Int source;
    public int pathRange;

    public WeightGraph(Tilemap map, Entity entity)
    {
        this.map = map;
        this.source = entity.Position;
        this.pathRange = (int) entity.speed;
    }

    public HashSet<Vector3Int> GetMovementRange(Entity entity)
    {
        //Offset (from 0 to pathRange)
        //Where start position is center of this matrix
        double[,] cost = new double[pathRange * 2 + 1, pathRange * 2 + 1];

        HashSet<Vector3Int> UIHighlight = new HashSet<Vector3Int>();
        HashSet<Vector3Int> tempUIHighlight = new HashSet<Vector3Int>();
        HashSet<Vector3Int> finalMovementHighlight = new HashSet<Vector3Int>();

        Vector3Int start = entity.Position;
        finalMovementHighlight.Add(start);

        //TODO: Can this generate error? Try to add or substract 1 from x,y
        Vector3Int OffsetPosition = new Vector3Int(start.x - pathRange, start.y - pathRange, start.z);

        foreach (Vector3Int v3 in map.GetTileRange(start, 1))
        {
            TileDefinition tile = DefinitionRegistry.Instance.Find(map.GetTileName(v3));
            if (tile == null)
            {
                Debug.LogError($"No tile definition for {map.GetTileName(v3)}");
            }

            Vector3Int matrixIndex = GetOffsetPosition(OffsetPosition, v3);

            if(v3 == start)
            {
                continue;
            }

            cost[matrixIndex.x, matrixIndex.y] = entity.Definition.GetMovementCost(tile.type);
            if (pathRange - cost[matrixIndex.x, matrixIndex.y] >= 0)
            {
                UIHighlight.Add(v3);
            }
        }

        finalMovementHighlight.UnionWith(UIHighlight);

        int attempts = 0;
        while (UIHighlight.Count != 0 && attempts <= 100)
        {
            attempts++;
            foreach (Vector3Int v3 in UIHighlight)
            {
                Vector3Int matrixIndex = GetOffsetPosition(OffsetPosition, v3);

                foreach (Vector3Int neighbour in map.GetTileRange(v3, 1))
                {
                    if (finalMovementHighlight.Contains(neighbour)) continue;

                    Vector3Int neighbourMatrixIndex = GetOffsetPosition(OffsetPosition, neighbour);

                    if(neighbourMatrixIndex.x >= pathRange*2+1 || neighbourMatrixIndex.y >= pathRange * 2 + 1)
                    {
                        continue;
                    }

                    TileDefinition tile = DefinitionRegistry.Instance.Find(map.GetTileName(neighbour));
                    if (tile == null)
                    {
                        Debug.LogError($"No tile definition for {map.GetTileName(neighbour)}");
                    }

                    cost[neighbourMatrixIndex.x, neighbourMatrixIndex.y] = entity.Definition.GetMovementCost(tile.type) + cost[matrixIndex.x, matrixIndex.y];
                    if (pathRange - cost[neighbourMatrixIndex.x, neighbourMatrixIndex.y] >= 0)
                    {
                        tempUIHighlight.Add(neighbour);
                    }
                }
            }

            UIHighlight = tempUIHighlight;
            finalMovementHighlight.UnionWith(UIHighlight);
            tempUIHighlight = new HashSet<Vector3Int>();
        }

        #region Debug
        /*GameObject prefab = GameObject.FindGameObjectWithTag("TextMesh");
        for (int x = 0; x < cost.GetLength(0); x++)
        {
            for (int y = 0; y < cost.GetLength(1); y++)
            {
                Vector3Int Pos = new Vector3Int(OffsetPosition.x + x, OffsetPosition.y + y, 0);

                Vector3 center = map.ToVector3Center(Pos);
                GameObject inst = MonoBehaviour.Instantiate(prefab, new Vector3(center.x, center.y, -1f), Quaternion.identity);
                inst.GetComponent<TextMesh>().text = $"{cost[x, y]}";
            }
        }

        foreach (Vector3Int canGo in finalMovementHighlight)
        {
            Vector3 local = map.ToVector3(canGo);
            Debug.DrawLine(local, new Vector3(local.x + 1f, local.y + 1f, local.z), Color.red);
            //Debug.Break();
        }*/
        #endregion
        return finalMovementHighlight;
    }

    /// <summary>
    /// Offset is used for getting the index of a matrix
    /// </summary>
    /// <param name="OffsetStart">Offset start always should have the lowest numbers (Left bot corner)</param>
    /// <param name="current"></param>
    /// <returns></returns>
    private Vector3Int GetOffsetPosition(Vector3Int OffsetStart, Vector3Int current)
    {
        return new Vector3Int(current.x - OffsetStart.x, current.y - OffsetStart.y, current.z);
    }

}
