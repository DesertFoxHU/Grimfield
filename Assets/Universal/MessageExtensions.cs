using Newtonsoft.Json;
using Riptide;
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

    public static Message AddBuildingByByte(this Message message, AbstractBuilding building)
    {
        byte[] array = building.ToByteArray();
        return message.Add(array.Length).Add(array);
    }

    public static AbstractBuilding GetBuildingByByte(this Message message)
    {
        int length = message.GetInt();
        return (AbstractBuilding) message.GetBytes(length).ToObject();
    }

    public static Message AddByByte(this Message message, AbstractBuilding value) => AddBuildingByByte(message, value);

    public static Message AddBuilding(this Message message, AbstractBuilding building)
    {
        string compressed = StringCompressor.CompressString(JsonConvert.SerializeObject(building, settings));
        return message.Add(compressed);
    }

    public static AbstractBuilding GetBuilding(this Message message)
    {
        return JsonConvert.DeserializeObject<AbstractBuilding>(StringCompressor.DecompressString(message.GetString()), settings);
    }

    public static Message Add(this Message message, AbstractBuilding value) => AddBuilding(message, value);

    #region Vector2
    /// <inheritdoc cref="AddVector2(Message, Vector2)"/>
    /// <remarks>This method is simply an alternative way of calling <see cref="AddVector2(Message, Vector2)"/>.</remarks>
    public static Message Add(this Message message, Vector2 value) => AddVector2(message, value);

    /// <summary>Adds a <see cref="Vector2"/> to the message.</summary>
    /// <param name="value">The <see cref="Vector2"/> to add.</param>
    /// <returns>The message that the <see cref="Vector2"/> was added to.</returns>
    public static Message AddVector2(this Message message, Vector2 value)
    {
        return message.AddFloat(value.x).AddFloat(value.y);
    }

    /// <summary>Retrieves a <see cref="Vector2"/> from the message.</summary>
    /// <returns>The <see cref="Vector2"/> that was retrieved.</returns>
    public static Vector2 GetVector2(this Message message)
    {
        return new Vector2(message.GetFloat(), message.GetFloat());
    }
    #endregion

    #region Vector3
    /// <inheritdoc cref="AddVector3(Message, Vector3)"/>
    /// <remarks>This method is simply an alternative way of calling <see cref="AddVector3(Message, Vector3)"/>.</remarks>
    public static Message Add(this Message message, Vector3 value) => AddVector3(message, value);

    /// <summary>Adds a <see cref="Vector3"/> to the message.</summary>
    /// <param name="value">The <see cref="Vector3"/> to add.</param>
    /// <returns>The message that the <see cref="Vector3"/> was added to.</returns>
    public static Message AddVector3(this Message message, Vector3 value)
    {
        return message.AddFloat(value.x).AddFloat(value.y).AddFloat(value.z);
    }

    /// <summary>Retrieves a <see cref="Vector3"/> from the message.</summary>
    /// <returns>The <see cref="Vector3"/> that was retrieved.</returns>
    public static Vector3 GetVector3(this Message message)
    {
        return new Vector3(message.GetFloat(), message.GetFloat(), message.GetFloat());
    }
    #endregion

    #region Quaternion
    /// <inheritdoc cref="AddQuaternion(Message, Quaternion)"/>
    /// <remarks>This method is simply an alternative way of calling <see cref="AddQuaternion(Message, Quaternion)"/>.</remarks>
    public static Message Add(this Message message, Quaternion value) => AddQuaternion(message, value);

    /// <summary>Adds a <see cref="Quaternion"/> to the message.</summary>
    /// <param name="value">The <see cref="Quaternion"/> to add.</param>
    /// <returns>The message that the <see cref="Quaternion"/> was added to.</returns>
    public static Message AddQuaternion(this Message message, Quaternion value)
    {
        return message.AddFloat(value.x).AddFloat(value.y).AddFloat(value.z).AddFloat(value.w);
    }

    /// <summary>Retrieves a <see cref="Quaternion"/> from the message.</summary>
    /// <returns>The <see cref="Quaternion"/> that was retrieved.</returns>
    public static Quaternion GetQuaternion(this Message message)
    {
        return new Quaternion(message.GetFloat(), message.GetFloat(), message.GetFloat(), message.GetFloat());
    }
    #endregion
}
