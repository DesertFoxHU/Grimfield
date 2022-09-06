using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

[CreateAssetMenu(menuName = "Rendered Tile/GrimfieldTile", fileName = "GrimfieldTile")]
public class GrimfieldTile : Tile
{
    public bool isClaimed = false;
    public TileDefinition definition;
    public TileType Type
    {
        get => definition.type;
    }
    [HideInInspector] public int spriteIndex;

    public void Init(TileDefinition definition)
    {
        this.definition = definition;
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);
        try
        {
            tileData.sprite = definition.sprites[spriteIndex];
        }
        catch (IndexOutOfRangeException ex)
        {
            Debug.LogError($"Index was out of bounds, for: {definition} index: {spriteIndex}");
        }
        name = definition.type.ToString();
    }
}
