using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResponseType
{
    SUCCESS,
    FAILURE
}

public class Response<T>
{
    public ResponseType type;
    public T value;

    private Response()
    {
    }

    private Response(ResponseType type, T value)
    {
        this.type = type;
        this.value = value;
    }

    public static Response<T> Create(ResponseType type, T value)
    {
        return new Response<T>(type, value);
    }
}
