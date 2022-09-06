using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tile/TileDefinition", fileName = "TileDefinition")]
public class TileDefinition : ScriptableObject
{
    public TileType type;
    public string tileName;
    public string description;
    public Sprite[] sprites;

    public Sprite GetRandomSprite()
    {
        if (sprites.Length == 0) return null;
        else if (sprites.Length == 1) return sprites[0];
        else return sprites[Random.Range(0, sprites.Length)];
    }

    public int GetRandomSpriteIndex()
    {
        if (sprites.Length == 0 || sprites.Length == 1) return 0;
        else return Random.Range(0, sprites.Length);
    }
}
