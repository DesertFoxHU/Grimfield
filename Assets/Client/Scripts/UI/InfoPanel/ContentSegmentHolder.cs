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
            if (building.GetType().GetMethod("InvokeRecruit") != null) prefabs.Add(GetPrefab("INVOKE_RECRUIT"));

            prefabs.Add(GetPrefab("STAT"));
            return prefabs;
        }

        public List<ContentSegment> GetPrefabs(Entity entity)
        {
            List<ContentSegment> prefabs = new List<ContentSegment>();
            prefabs.Add(GetPrefab("ENTITY_STAT"));
            prefabs.Add(GetPrefab("ENTITY_OWNER"));
            prefabs.Add(GetPrefab("ENTITY_ID"));
            return prefabs;
        }
    }
}