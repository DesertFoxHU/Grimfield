using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUtils
{
    public static SquareView GetSquareOfView(Camera camera)
    {
        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Vector3 botLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        TileUtils utils = new TileUtils(GameObject.FindGameObjectWithTag("GameMap"));
        return new SquareView(topRight, botLeft, utils);
    }
}

public class SquareView
{
    public Vector3Int topRight { get; private set; }
    public Vector3Int botLeft { get; private set; }

    public SquareView(Vector3 topRight, Vector3 botLeft, TileUtils utils)
    {
        this.topRight = utils.ToVector3Int(topRight);
        this.botLeft = utils.ToVector3Int(botLeft);
    }

    public override string ToString()
    {
        return "(" + topRight.x + "," + topRight.y + ")(" + botLeft.x + ";" + botLeft.y + ")";
    }
}
