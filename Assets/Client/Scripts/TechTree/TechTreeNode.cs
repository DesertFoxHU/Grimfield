using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechTreeNode : MonoBehaviour
{
    public List<TechTreeNode> parents;
    public bool isUnlocked;

    private void OnValidate()
    {
        
    }
}
