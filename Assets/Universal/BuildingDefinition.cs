using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tile/BuildingDefinition", fileName = "BuildingDefinition")]
public class BuildingDefinition : ScriptableObject
{
    public BuildingType type;
    public Sprite[] spritesLevel;
    public List<TileType> placeable;
    public bool hasProductStorage;

    #region ProducerBuilding
    public bool isProducer;
    /*[Conditional("isProducer", true)]*/ public ResourceType produceType;
    /*[Conditional("isProducer", true)]*/ public List<ValuePair> ProduceLevel;
    #endregion

    public Sprite GetSpriteByLevel(int level)
    {
        return spritesLevel[level-1];
    }
}

[System.Serializable]
public struct ValuePair
{
    public int level;
    public double value;
}
