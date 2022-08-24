using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private EntityDefinition Definition { get; set; }
    public EntityType Type
    {
        get => Definition.Type;
    }
    public bool IsInitialized { get; private set; } = false;
    public ushort OwnerId { get; private set; }
    public double health;
    public double damage;
    public double speed;

    #region Debug
    public void Start()
    {
        Initialize(FindObjectOfType<DefinitionRegistry>().Find(EntityType.Skeleton));
    }
    #endregion

    public void Initialize(EntityDefinition definition)
    {
        Definition = definition;
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
