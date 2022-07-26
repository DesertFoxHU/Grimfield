﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraMoving : MonoBehaviour
{
    [SerializeField] public float step = 0.1f;

    void Update()
    {
        Vector3 before = this.GetComponentInParent<Camera>().transform.position;
        Vector3 v = this.GetComponentInParent<Camera>().transform.position;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            v.x -= step;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            v.x += step;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            v.y += step;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            v.y -= step;
        }

        if (IsCameraOut(v))
        {
            return;
        }

        CursorVisual.ShowCursor();
        this.GetComponentInParent<Camera>().transform.position = v;
    }

    private bool IsCameraOut(Vector3 vector)
    {
        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Vector3 botLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));

        Tilemap map = GameObject.FindGameObjectWithTag("GameMap").GetComponent<Tilemap>();
        BoundsInt bounds = map.cellBounds;

        Vector3 max = new Vector3(bounds.size.x, bounds.size.y-1, 0f);
        Vector3 min = new Vector3(0f, 0f, 0f);

        //Allow leaving the zone a bit for the camera
        max.Set(max.x + 5f, max.y + 5f, 0f);
        min.Set(min.x - 5f, min.y - 5f, 0f);

        bool changed = false;
        if(topRight.x >= max.x)
        {
            vector.x -= step;
            changed = true;
        }
        if(botLeft.x <= min.x)
        {
            vector.x += step;
            changed = true;
        }

        if (topRight.y >= max.y)
        {
            vector.y -= step;
            changed = true;
        }
        if (botLeft.y <= min.y)
        {
            vector.y += step;
            changed = true;
        }
        this.GetComponentInParent<Camera>().transform.position = vector;
        return changed;
    }
}
