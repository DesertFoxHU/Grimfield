using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;

public static class ByteSerializator
{
    public static byte[] ToByteArray(this Object obj)
    {
        if (obj == null) return null;

        BinaryFormatter bf = new BinaryFormatter();
        MemoryStream ms = new MemoryStream();
        bf.Serialize(ms, obj);

        return ms.ToArray();
    }

    public static Object ToObject(this byte[] array)
    {
        MemoryStream memStream = new MemoryStream();
        BinaryFormatter binForm = new BinaryFormatter();
        memStream.Write(array, 0, array.Length);
        memStream.Seek(0, SeekOrigin.Begin);
        Object obj = (Object)binForm.Deserialize(memStream);
        return obj;
    }
}
