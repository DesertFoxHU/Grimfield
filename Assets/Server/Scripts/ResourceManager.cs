using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ServerSide
{
    public class ResourceManager : MonoBehaviour
    {
        private double timer = 0.5f;

        void Update()
        {
            if (NetworkManager.Instance.State != ServerState.Playing) return;

            timer -= Time.deltaTime;
            if(timer < 0)
            {
                timer = 0.5f;
                foreach(ServerPlayer player in NetworkManager.players)
                {
                    ServerSender.UpdatePlayerResource(player, player.GetAvaibleResources());
                }
            }
        }
    }
}