using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tile/TileDefinition", fileName = "TileDefinition")]
public class TileDefiniton : ScriptableObject
{
    public TileType type;
    public Sprite[] sprites;
}
