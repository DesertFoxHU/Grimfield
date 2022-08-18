using Riptide;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ServerSide
{
    public class Lobby
    {
        public List<Color> AvaibleColor;

        public Lobby()
        {
            if(NetworkManager.Instance.Server.MaxClientCount > NetworkManager.Instance.PlayerColors.Count)
            {
                Debug.LogWarning("Not enough color is predefined for every player!");
            }

            AvaibleColor = new List<Color>(NetworkManager.Instance.PlayerColors);
        }

        public Color GetAnAvaibleColor()
        {
            if(AvaibleColor.Count == 0)
            {
                Debug.LogError("No more avaible colors for players!\n Please restart the server and add more possible color variations!");
                return Color.black;
            }

            int index = Random.Range(0, AvaibleColor.Count);
            Color color = AvaibleColor[index];
            AvaibleColor.RemoveAt(index);
            return color;
        }
    }
}