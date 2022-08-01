using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] public float sensitivity = 2f;
    [SerializeField] public float maxZoomIn = 3.5f;
    [SerializeField] public float maxZoomOut = 8f;

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") == 0)
        {
            return;
        }

        float size = this.GetComponentInParent<Camera>().orthographicSize;
        float before = size;
        size += -Input.GetAxis("Mouse ScrollWheel") * sensitivity;

        if (size <= maxZoomIn)
        {
            size = maxZoomIn;
        }
        if (size >= maxZoomOut)
        {
            size = maxZoomOut;
        }

        this.GetComponentInParent<Camera>().orthographicSize = size;
        if (IsCameraOut())
        {
            this.GetComponentInParent<Camera>().orthographicSize = before;
        }
        CursorVisual.ShowCursor();
    }

    private bool IsCameraOut()
    {
        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Vector3 botLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));

        Tilemap map = GameObject.FindGameObjectWithTag("GameMap").GetComponent<Tilemap>();
        BoundsInt bounds = map.cellBounds;

        Vector3 max = new Vector3(bounds.size.x, bounds.size.y - 1, 0f);
        Vector3 min = new Vector3(0f, 0f, 0f);

        bool changed = false;
        if (topRight.x >= max.x)
        {
            changed = true;
        }
        if (botLeft.x <= min.x)
        {
            changed = true;
        }

        if (topRight.y >= max.y)
        {
            changed = true;
        }
        if (botLeft.y <= min.y)
        {
            changed = true;
        }
        return changed;
    }
}
