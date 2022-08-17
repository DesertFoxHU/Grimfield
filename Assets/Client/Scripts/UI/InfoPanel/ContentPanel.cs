using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InfoPanel
{

    /// <summary>
    /// ContentPanel is inside of InfoPanel and controls filling data and processing them
    /// </summary>
    public class ContentPanel : MonoBehaviour
    {
        public InfoWindow.ContentType type;
        private ContentSegmentHolder holder;

        public void Start()
        {
            holder = FindObjectOfType<ContentSegmentHolder>();
        }

        public void Load(object obj)
        {
            ClearChildren();
            float Y = 0;
            if(type == InfoWindow.ContentType.Building)
            {
                AbstractBuilding building = (AbstractBuilding) obj;

                //These are just prefabs
                List<ContentSegment> prefabs = holder.GetPrefabs(building);
                prefabs = prefabs.OrderBy(x => x.order).ToList();

                foreach(ContentSegment segment in prefabs)
                {
                    CreateNewInstance(segment.gameObject, Y);
                    Y -= segment.height;
                }
            }
        }

        public void ClearChildren()
        {
            foreach(Transform children in this.transform)
            {
                if (children.gameObject.activeSelf)
                {
                    Destroy(children.gameObject);
                }
            }
        }

        public GameObject CreateNewInstance(GameObject prefab, float Y)
        {
            GameObject newInstance = Instantiate(prefab, new Vector3(0, Y, 0), Quaternion.identity);
            newInstance.transform.SetParent(this.transform, false);
            newInstance.SetActive(true);
            return newInstance;
        }
    }
}