using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResponseType
{
    SUCCESS,
    FAILURE
}

public class Response<ResponseType, T>
{
    public ResponseType type;
    public T value;

    public Response()
    {
    }

    public Response(ResponseType type, T value)
    {
        this.type = type;
        this.value = value;
    }
}
