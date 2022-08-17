using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfoPanel
{
    public class ContentSegmentHolder : MonoBehaviour
    {
        public List<ContentSegment> segmentsPrefab;

        public ContentSegment GetPrefab(string id)
        {
            return segmentsPrefab.Find(x => x.ID == id);
        }

        public List<ContentSegment> GetPrefabs(AbstractBuilding building)
        {
            List<ContentSegment> prefabs = new List<ContentSegment>();
            if (building is Village) prefabs.Add(GetPrefab("CAPITAL"));
            if (building.GetDefinition().hasProductStorage) prefabs.Add(GetPrefab("RES_STORAGE"));

            prefabs.Add(GetPrefab("STAT"));
            return prefabs;
        }
    }
}