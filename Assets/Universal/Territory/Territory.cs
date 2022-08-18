using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

public class Territory
{
    public ushort ClientID = 0;
    public List<Vector3Int> ClaimedLand { get; set; } = new List<Vector3Int>();

    public Territory() {}

    public Territory(List<Vector3Int> claimedLand)
    {
        ClaimedLand = claimedLand;
    }

    public Territory(ushort clientID, List<Vector3Int> claimedLand)
    {
        ClientID = clientID;
        ClaimedLand = claimedLand;
    }
}
