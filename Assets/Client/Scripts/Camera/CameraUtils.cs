using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraUtils
{
    public static SquareView GetSquareOfView(Camera camera)
    {
        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Vector3 botLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        return new SquareView(topRight, botLeft);
    }
}

public class SquareView
{
    public Vector3Int topRight { get; private set; }
    public Vector3Int botLeft { get; private set; }
    public Tilemap map;

    public SquareView(Vector3 topRight, Vector3 botLeft)
    {
        map = GameObject.FindGameObjectWithTag("GameMap").GetComponent<Tilemap>();
        this.topRight = map.ToVector3Int(topRight);
        this.botLeft = map.ToVector3Int(botLeft);
    }

    public override string ToString()
    {
        return "(" + topRight.x + "," + topRight.y + ")(" + botLeft.x + ";" + botLeft.y + ")";
    }
}
