using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Entity/EntityDefinition", fileName = "EntityDefinition")]
public class EntityDefinition : ScriptableObject
{
    public enum TerrainMovementType
    {
        Ground,
        Air,
        Water
    }

    public EntityType Type;
    public GameObject Prefab;
    public Sprite RecruitIcon;
    public string Name;
    public string Description;
    public List<double> Health;
    public List<double> Damage;
    public List<double> Speed;
    public List<int> attackRange;
    public TerrainMovementType MovementType;
    public List<ResourceHolder> RecruitCost;
    public List<ResourceHolder> Upkeep;
    [SerializeField] private List<ValuePair<TileType, double>> MovementCost;

    #region Animation related
    public AnimatorValues animatorValues;
    #endregion

    public double GetMovementCost(TileType type)
    {
        foreach(ValuePair<TileType, double> cost in MovementCost)
        {
            if(cost.Value1 == type)
            {
                return cost.Value2;
            }
        }
        return 1;
    }

    public Dictionary<ResourceType, double> GetRecruitCost()
    {
        return RecruitCost.ToDictionary(x => x.type, y => y.Value);
    }

    public Dictionary<ResourceType, double> GetUpkeep()
    {
        return Upkeep.ToDictionary(x => x.type, y => y.Value);
    }

    private void OnValidate()
    {
        for (int i = 0; i < MovementCost.Count; i++)
        {
            ValuePair<TileType, double> cost = MovementCost[i];
            if (cost.Value2 < 1)
            {
                cost.Value2 = 1;
            }
        }
    }
}

[System.Serializable]
public struct ValuePair<T1, T2>
{
    public T1 Value1;
    public T2 Value2;
}

/// <summary>
/// If you read the values from the animator then consider their exchange values
/// For example if you read 60 then thats mean 60 frame (or 1 seconds), so there is no problem with that
/// But when you read 53 then it's a problem, you can't just put 0.53 there.
/// So divide the value by 60 !
/// </summary>
[System.Serializable]
public class AnimatorValues
{
    [Tooltip("The length of time until the animation will show an attack frame. In seconds")] public float attackStartTime;
    [Tooltip("End time of the attacking in seconds. Or when to start moving back to its original place")] public float attackEndTime;
}
