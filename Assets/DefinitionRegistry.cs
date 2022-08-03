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

    public BuildingDefinition Find(BuildingType type)
    {
        return Buildings.Find(a => a.type == type);
    }

    public TileDefiniton Find(TileType type)
    {
        return Tiles.Find(a => a.type == type);
    }
}
