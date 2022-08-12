using Newtonsoft.Json;
using RiptideNetworking;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class MessageExtensions
{
    public static JsonSerializerSettings settings = new JsonSerializerSettings()
    {
        TypeNameHandling = TypeNameHandling.All
    };

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
        return message.Add(JsonConvert.SerializeObject(building, settings));
    }

    public static AbstractBuilding GetBuilding(this Message message)
    {
        return JsonConvert.DeserializeObject<AbstractBuilding>(message.GetString(), settings);
    }

    public static Message Add(this Message message, AbstractBuilding value) => AddBuilding(message, value);
}
