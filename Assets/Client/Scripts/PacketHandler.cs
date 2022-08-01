using RiptideNetworking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PacketHandler : MonoBehaviour
{
    [MessageHandler((ushort)ServerToClientPacket.SendAlert)]
    private static void OnAlertRecieve(Message message)
    {
        string msg = message.GetString();
        FindObjectOfType<MessageDisplayer>().SetMessage(msg);
    }

    [MessageHandler((ushort)ServerToClientPacket.LoadLobby)]
    private static void LobbyLoad(Message message)
    {
        SceneManager.LoadScene("LobbyScene", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(1);
    }

    [MessageHandler((ushort)ServerToClientPacket.UpdateLobby)]
    private static void LobbyUpdate(Message message)
    {
        int count = message.GetInt();
        ushort maxPlayer = message.GetUShort();

        foreach(GameObject go in GameObject.FindGameObjectsWithTag("LobbyPlayersText")){
            go.GetComponent<TMPro.TextMeshProUGUI>().text = $"Players: {count}/{maxPlayer}";
        }

        List<string> raw = new List<string>();
        for(int i = 0; i < count; i++)
        {
            raw.Add(message.GetString());
        }

        LobbyPlayerList playerList = FindObjectOfType<LobbyPlayerList>();
        if (playerList != null) playerList.UpdateList(raw);
    }
}
