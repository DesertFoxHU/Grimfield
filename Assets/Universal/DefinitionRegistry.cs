using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefinitionRegistry : MonoBehaviour
{
    public static DefinitionRegistry Instance;

    void Start()
    {
        Instance = this;
    }

    public List<TileDefiniton> Tiles;
    public List<BuildingDefinition> Buildings;
    public List<EntityDefinition> Entities;

    public BuildingDefinition Find(BuildingType type)
    {
        return Buildings.Find(a => a.type == type);
    }

    public TileDefiniton Find(TileType type)
    {
        return Tiles.Find(a => a.type == type);
    }

    public EntityDefinition Find(EntityType type)
    {
        return Entities.Find(a => a.Type == type);
    }

    public TileDefiniton Find(string spriteName)
    {
        foreach (TileDefiniton tf in Tiles)
        {
            foreach(Sprite sprite in tf.sprites)
            {
                if (sprite.name == spriteName) return tf;
            }
        }
        return null;
    }
}
