using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Entity/EntityDefinition", fileName = "EntityDefinition")]
public class EntityDefinition : ScriptableObject
{
    public EntityType Type;
    public GameObject Prefab;
    public Sprite RecruitIcon;
    public string Name;
    public List<double> Healths;
    public List<double> Damage;
    public List<double> Speed;
}
