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
            { "GetMapDimension", GetMapDimension },
            { "SpawnUnit", SpawnUnit }
        };

        #region Definitions
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

        //SpawnUnit [PlayerName] [Pos] [EntityType]
        public static Response<string> SpawnUnit(string[] args, string fullCommand)
        {
            string name = args[0].Trim();
            string pos = args[1];

            ServerPlayer player = NetworkManager.players.Find(x => x.Name == name);
            if(player == null)
            {
                return Response<string>.Create(ResponseType.FAILURE, $"There is no player named {name}");
            }

            if (!pos.Contains(','))
            {
                return Response<string>.Create(ResponseType.FAILURE, $"The position format is incorrect. Correct usage: x,y");
            }

            int x = 0;
            int y = 0;
            try
            {
                x = int.Parse(pos.Split(',')[0]);
                y = int.Parse(pos.Split(',')[1]);
            }
            catch
            {
                return Response<string>.Create(ResponseType.FAILURE, $"Can't parse position. Input was: {pos}");
            }

            if(!Enum.IsDefined(typeof(EntityType), args[2]))
            {
                return Response<string>.Create(ResponseType.FAILURE, $"There is no EntityType named {args[2]}");
            }

            EntityType type = (EntityType) Enum.Parse(typeof(EntityType), args[2]);
            GameController.Instance.SpawnUnit(player, new Vector3Int(x, y, 0), type);
            return Response<string>.Create(ResponseType.SUCCESS, $"Spawned {type} on {new Vector3Int(x, y, 0)} for {name}");
        }
        #endregion
    }
}