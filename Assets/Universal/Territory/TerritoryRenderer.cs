using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

public class TerritoryRenderer : MonoBehaviour
{
    [Flags]
    public enum Sides
    {
        None = 1,
        Left = 2,
        Right = 4,
        Top = 8,
        Bottom = 16,
        ALL = Left | Right | Top | Bottom,
    }

    public static TerritoryRenderer Instance;
    public Color color;
    private Tilemap map;
    private List<AbstractBuilding> buildings = new List<AbstractBuilding>();

    public GameObject spritePrefab;
    [Tooltip("0: Left line, 1: left-upper corner, 2: left-upper-bottom corner, 3: all corner")] public Sprite[] sprites;

    public void Start()
    {
        Instance = this;

        map = GameObject.FindGameObjectWithTag("GameMap").GetComponent<Tilemap>();

        /*AbstractBuilding building = new Village(new ServerSide.ServerPlayer(0, "Test1"), new Vector3Int(15, 10, 0));
        buildings.Add(building);
        building.OnClaimLand(map);

        AbstractBuilding building2 = new Village(new ServerSide.ServerPlayer(1, "Test2"), new Vector3Int(20, 14, 0));
        buildings.Add(building2);
        building2.OnClaimLand(map);*/

        foreach (AbstractBuilding build in buildings)
        {
            Render(build.ClaimedLand, color);
        }
    }

    public void Render(List<Vector3Int> lands, Color color)
    {
        foreach (Vector3Int land in lands)
        {
            Vector3 pos = map.ToVector3(land);

            Sides drawSides = Sides.None;

            if (!lands.Contains(new Vector3Int(land.x - 1, land.y))) //Empty left
            {
                drawSides |= Sides.Left;
            }

            if (!lands.Contains(new Vector3Int(land.x + 1, land.y))) //Empty right
            {
                drawSides |= Sides.Right;
            }

            if (!lands.Contains(new Vector3Int(land.x, land.y + 1))) //Empty upper
            {
                drawSides |= Sides.Top;
            }

            if (!lands.Contains(new Vector3Int(land.x, land.y - 1))) //Empty bottom
            {
                drawSides |= Sides.Bottom;
            }

            if (drawSides != Sides.None)
            {
                drawSides &= ~Sides.None; //Remove 'None' tag 
            }
            else continue; //Continue, because we dont need to draw anything

            GameObject go = Instantiate(spritePrefab, new Vector3(pos.x + .5f, pos.y + .5f, -1.1f), Quaternion.identity);
            go.transform.SetParent(this.transform, false);
            SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
            sr.color = color;

            if (drawSides == Sides.ALL)
            {
                sr.sprite = sprites[3];
            }
            else if(drawSides == (Sides.Top | Sides.Left))
            {
                sr.sprite = sprites[1];
            }
            else if (drawSides == (Sides.Top | Sides.Right))
            {
                sr.sprite = sprites[1];
                go.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 270f));
            }
            else if (drawSides == (Sides.Bottom | Sides.Left))
            {
                sr.sprite = sprites[1];
                go.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
            }
            else if (drawSides == (Sides.Bottom | Sides.Right))
            {
                sr.sprite = sprites[1];
                go.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
            }
            else if(drawSides ==  Sides.Left)
            {
                sr.sprite = sprites[0];
            }
            else if (drawSides == Sides.Bottom)
            {
                sr.sprite = sprites[0];
                go.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
            }
            else if (drawSides == Sides.Top)
            {
                sr.sprite = sprites[0];
                go.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 270f));
            }
            else if (drawSides == Sides.Right)
            {
                sr.sprite = sprites[0];
                go.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
            }
        }
    }

    /*public void Render(Tilemap map, List<Vector3Int> lands, Color color)
    {
        GameObject go = new GameObject("LAND_RENDERER");
        go.transform.SetParent(this.gameObject.transform, false);
        go.transform.position = new Vector3(0f, 0f, -1.1f);

        LineRenderer line = go.AddComponent<LineRenderer>();

        Gradient gradient = new Gradient();

        GradientColorKey[] keys = new GradientColorKey[1];
        keys[0].color = color;

        GradientAlphaKey[] akeys = new GradientAlphaKey[1];
        akeys[0].alpha = 1f;

        gradient.SetKeys(keys, akeys);

        line.colorGradient = gradient;
        line.widthCurve = new AnimationCurve(new Keyframe(0f, 0.1f), new Keyframe(1f, 0.1f));

        line.positionCount = lands.Count;
        int index = 0;
        foreach(Vector3Int land in lands)
        {
            Vector3 pos = map.ToVector3(land);
            Draw(ref index, ref line, new Vector3(pos.x + 0.5f, pos.y + 0.5f, -1.1f));
            if (!lands.Contains(new Vector3Int(land.x - 1, land.y))) //Empty left
            {
                Draw(ref index, ref line, new Vector3(pos.x, pos.y, -1.1f));
                Draw(ref index, ref line, new Vector3(pos.x, pos.y + 1f, -1.1f));
            }

            if (!lands.Contains(new Vector3Int(land.x + 1, land.y))) //Empty right
            {
                Draw(ref index, ref line, new Vector3(pos.x + 1f, pos.y, -1.1f));
                Draw(ref index, ref line, new Vector3(pos.x + 1f, pos.y + 1f, -1.1f));
            }

            if (!lands.Contains(new Vector3Int(land.x, land.y + 1))) //Empty upper
            {
                Draw(ref index, ref line, new Vector3(pos.x, pos.y + 1f, -1.1f));
                Draw(ref index, ref line, new Vector3(pos.x + 1f, pos.y + 1f, -1.1f));
            }

            if (!lands.Contains(new Vector3Int(land.x, land.y + 1))) //Empty bottom
            {
                Draw(ref index, ref line, new Vector3(pos.x, pos.y, -1.1f));
                Draw(ref index, ref line, new Vector3(pos.x + 1f, pos.y, -1.1f));
            }
        }
    }*/

    private void Draw(ref int index, ref LineRenderer render, Vector3 position)
    {
        render.SetPosition(index, position);
        index++;
    }

    private static bool IsBorder(List<Vector3Int> possibilities, Vector3Int pos)
    {
        if (!possibilities.Contains(new Vector3Int(pos.x + 1, pos.y))) return true;
        else if (!possibilities.Contains(new Vector3Int(pos.x - 1, pos.y))) return true;
        else if (!possibilities.Contains(new Vector3Int(pos.x, pos.y + 1))) return true;
        else if (!possibilities.Contains(new Vector3Int(pos.x, pos.y - 1))) return true;
        return false;
    }
}
