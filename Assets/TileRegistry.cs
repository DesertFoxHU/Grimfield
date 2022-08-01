using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRegistry : MonoBehaviour
{
    public static TileRegistry instance;

    private void Start()
    {
        instance = this;
    }

    [Serializable]
    public struct SpriteElement
    {
        public TileType Type;
        public Sprite Sprite;
    }

    [SerializeField] public List<SpriteElement> SpriteRegistry;

    public Sprite GetSpriteByType(TileType type)
    {
        return SpriteRegistry.Find(a => a.Type == type).Sprite;
    }
}
