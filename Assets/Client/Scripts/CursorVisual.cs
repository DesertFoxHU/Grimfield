using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CursorVisual : MonoBehaviour
{
    private int anim = 1;
    public Sprite anim1;
    public Sprite anim2;

    public static GameObject go;
    private static Tilemap map;

    void Start()
    {
        go = new GameObject("CursorAim");
        go.SetActive(false);
        go.hideFlags = HideFlags.HideInHierarchy;
        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = anim1;

        map = GameObject.FindGameObjectWithTag("GameMap").GetComponent<Tilemap>();
    }

    private float elapsed;

    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 0.5f)
        {
            ChangeAnim();
            elapsed = 0f;
        }

        if (IsMouseMoved())
        {
            ShowCursor();
        }

    }

    public static void ShowCursor()
    {
        if (Utils.IsPointerOverUIElement())
        {
            go.SetActive(false);
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
        Vector3Int pos = map.ToVector3Int(worldPoint);

        if (!map.HasTile(pos))
        {
            go.SetActive(false);
        }

        worldPoint = map.ToVector3(pos);
        worldPoint.Set(worldPoint.x + 0.5f, worldPoint.y + 0.5f, -1.1f);
        go.SetActive(true);
        go.transform.position = worldPoint;
    }

    private bool IsMouseMoved()
    {
        return (Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0);
    }

    private void ChangeAnim()
    {
        if (go == null)
        {
            return;
        }

        if (anim == 1)
        {
            anim = 2;
            go.GetComponent<SpriteRenderer>().sprite = anim2;
        }
        else if (anim == 2)
        {
            anim = 1;
            go.GetComponent<SpriteRenderer>().sprite = anim1;
        }
    }
}
