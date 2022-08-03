using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tile/BuildingDefinition", fileName = "BuildingDefinition")]
public class BuildingDefinition : ScriptableObject
{
    public BuildingType type;
    public Sprite[] spritesLevel;

    public Sprite GetSpriteByLevel(int level)
    {
        return spritesLevel[level-1];
    }
}
