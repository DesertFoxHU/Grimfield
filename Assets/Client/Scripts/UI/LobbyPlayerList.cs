using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPlayerList : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public Sprite ReadySprite;
    public Sprite NotReadySprite;

    public void UpdateList(List<string> raw)
    {
        foreach (Transform child in this.transform)
        {
            GameObject go = child.gameObject;
            if (!go.name.StartsWith("Player")) continue;
            Destroy(go);
        }

        int Y = 0;
        foreach (string s in raw)
        {
            string[] splits = s.Split('|');
            if (splits.Length != 3) 
            {
                Debug.LogError("Invalid UpdateLobby format! Arguments length cannot be longer or less than 3");
                continue;
            }

            ushort playerId = ushort.Parse(splits[0]);
            string name = splits[1];
            bool isReady = bool.Parse(splits[2]);

            GameObject newInstance = Instantiate(PlayerPrefab, new Vector3(0, Y, 0), Quaternion.identity);
            newInstance.transform.SetParent(this.transform, false);
            newInstance.name = "Player_" + playerId;
            newInstance.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = name;

            foreach(Transform child in newInstance.transform)
            {
                if (child.name != "ReadyIcon") continue;
                child.GetComponent<Image>().sprite = isReady ? ReadySprite : NotReadySprite;
            }

            Y += -97;
        }
    }

    public GameObject Find(ushort PlayerId)
    {
        foreach(Transform child in this.transform)
        {
            GameObject go = child.gameObject;
            if (!go.name.StartsWith("Player")) continue;
            if (go.name == "Player_" + PlayerId) return go;
        }
        return null;
    }
}
