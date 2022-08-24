using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public EntityDefinition Definition { get; private set; }
    public EntityType Type
    {
        get => Definition.Type;
    }
    public bool IsInitialized { get; private set; } = false;
    public Vector3Int Position { get; private set; }
    public ushort OwnerId { get; private set; }
    public double health;
    public double damage;
    public double speed;

    #region Debug
    public void Start()
    {
        Initialize(new Vector3Int(0, 0, 0), FindObjectOfType<DefinitionRegistry>().Find(EntityType.Skeleton));
    }
    #endregion

    public void Initialize(Vector3Int Position, EntityDefinition definition)
    {
        Definition = definition;
        this.Position = Position;
        health = definition.Health[0];
        damage = definition.Damage[0];
        speed = definition.Speed[0];
        IsInitialized = true;
    }

    public void SetOwner(ushort clientID)
    {
        this.OwnerId = clientID;
    }
}
