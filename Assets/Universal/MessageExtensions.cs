using Newtonsoft.Json;
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

    public static Message AddBuilding(this Message message, AbstractBuilding building)
    {
        byte[] array = building.ToByteArray();
        return message.Add(array.Length).Add(array);
    }

    public static AbstractBuilding GetBuilding(this Message message)
    {
        int length = message.GetInt();
        return (AbstractBuilding) message.GetBytes(length).ToObject();
    }

    public static Message Add(this Message message, AbstractBuilding value) => AddBuilding(message, value);
}
