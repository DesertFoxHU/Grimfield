using RiptideNetworking;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class MessageExtensions
{
    public static Message AddGuid(this Message message, Guid ID)
    {
        return message.AddString(ID.ToString());
    }

    public static Guid GetGuid(this Message message)
    {
        return Guid.Parse(message.GetString());
    }

    public static Message Add(this Message message, Guid ID) => AddGuid(message, ID);

    public static Message AddVector3Int(this Message message, Vector3Int value)
    {
        return message.AddInt(value.x).AddInt(value.y).AddInt(value.z);
    }

    public static Vector3Int GetVector3Int(this Message message)
    {
        return new Vector3Int(message.GetInt(), message.GetInt(), message.GetInt());
    }

    public static Message Add(this Message message, Vector3Int value) => AddVector3Int(message, value);
}
