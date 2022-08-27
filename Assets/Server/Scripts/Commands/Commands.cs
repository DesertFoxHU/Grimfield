using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ServerSide
{
    public class Commands
    {
        public delegate Response<string> handle(string[] args, string fullCommand);

        public static Dictionary<string, handle> commandHandlers = new Dictionary<string, handle>(StringComparer.InvariantCultureIgnoreCase)
        {
            { "help", Help },
            { "GetMapDimension", GetMapDimension }
        };

        public static Response<string> Help(string[] args, string fullCommand)
        {
            string final = "";
            foreach(string key in commandHandlers.Keys)
            {
                final += "/" + key + "\n";
            }
            return Response<string>.Create(ResponseType.SUCCESS, final);
        }

        public static Response<string> GetMapDimension(string[] args, string fullCommand)
        {
            if (GameController.Instance == null)
            {
                return Response<string>.Create(ResponseType.FAILURE, $"The GameController is not yet created.");
            }
            return Response<string>.Create(ResponseType.SUCCESS, $"The current map is {GameController.Instance.SizeX}x{GameController.Instance.SizeY}");
        }
    }
}