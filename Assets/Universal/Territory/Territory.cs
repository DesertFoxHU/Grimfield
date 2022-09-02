using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Territory
{
    public int ID;
    public ushort ClientID = 0;
    public Dictionary<int, List<Vector3Int>> ClaimedLand { get; set; } = new Dictionary<int, List<Vector3Int>>();

    public Territory() {}

    public Territory(List<Vector3Int> claimedLand)
    {
        ProcessClaimedLand(claimedLand);
    }

    public Territory(ushort clientID, List<Vector3Int> claimedLand)
    {
        ClientID = clientID;
        ProcessClaimedLand(claimedLand);
    }

    private void ProcessClaimedLand(List<Vector3Int> claimedLand)
    {
        ClaimedLand.Clear();
        int index = 0;
        for (int i = 0; i < claimedLand.Count; i++)
        {
            if (i % 6 == 0)
            {
                index++;
            }

            if (!ClaimedLand.ContainsKey(index))
            {
                ClaimedLand.Add(index, new List<Vector3Int>());
            }

            ClaimedLand[index].Add(claimedLand[i]);
        }
    }

    public List<Vector3Int> GetAll()
    {
        return ClaimedLand.SelectMany(x => x.Value).ToList();
    }

    public abstract Color GetColor();
}
