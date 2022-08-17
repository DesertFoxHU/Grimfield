using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfoPanel
{

    /// <summary>
    /// ContentSegments are inside of ContentPanel
    /// The ContentPanel controls their show order and it handles their dynamic loading
    /// 
    /// Usually a Segment task is to load a specific piece of information, for example:
    /// - Resource Storage
    /// - Description
    /// 
    /// And because they are not the same height and not every building has the same property
    /// this class used to determine one of the property's height
    /// </summary>
    public class ContentSegment : MonoBehaviour
    {
        public string ID;
        public float height;
        [Tooltip("Order determines this segment priority to shown on top, higher value means higher priority")] public int order;
    }
}