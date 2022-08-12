using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CursorAction : MonoBehaviour
{
    private static Tilemap map;

    void Start()
    {
        map = GameObject.FindGameObjectWithTag("GameMap").GetComponent<Tilemap>();
    }

    void Update()
    {
        if (Utils.IsPointerOverUIElement()) return;
        if (map == null) return;

        if (Input.GetMouseButtonDown(0)) FireAction(MouseClickType.LeftClick);
        else if (Input.GetMouseButtonDown(1)) FireAction(MouseClickType.RightClick);
        else if(Input.GetMouseButtonDown(2)) FireAction(MouseClickType.MiddleClick);
    }

    private void FireAction(MouseClickType type)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
        Vector3Int pos = map.ToVector3Int(worldPoint);

        if (!map.HasTile(pos)) return;

        //TODO: Check Entity

        foreach (GameObject go in map.transform)
        {
            string name = go.name;
            if (name.StartsWith("Building"))
            {
                Vector3Int buildingPos = map.ToVector3Int(go.transform.position);
                if(buildingPos.x == pos.x && buildingPos.y == pos.y)
                {
                    ushort clientID = ushort.Parse(name.Split('_')[1]);
                    if (clientID != NetworkManager.Instance.ClientPlayer.ClientID) break;

                    string rawGuid = name.Split('_')[2];
                    ClientSender.SendBuildingFetchRequest(rawGuid, type);
                    return;
                }
            }
        }

        //TODO: Load tile
    }
}
