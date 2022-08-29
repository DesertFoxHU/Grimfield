using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceIconRegistry : MonoBehaviour
{
    [System.Serializable]
    public struct ResourceIcons
    {
        public ResourceType type;
        public Sprite icon;
    }

    public List<ResourceIcons> Icons;

    public Sprite Find(ResourceType type)
    {
        return Icons.Find(x => x.type == type).icon;
    }
}
