using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatPanel : MonoBehaviour
{
    public const int MaxMessage = 25;

    public GameObject chatPanel;
    public TMP_InputField inputField;
    public GameObject contentHolder;

    public GameObject textPrefab;

    private readonly List<TextMeshProUGUI> texts = new();

    public void AddMessage(string message)
    {
        if (texts.Count >= 25)
        {
            DeleteLastMessage();
        }

        GameObject newText = Instantiate(textPrefab, contentHolder.transform);
        TextMeshProUGUI text = newText.GetComponent<TextMeshProUGUI>();
        text.text = message;

        texts.Add(text);
    }

    public void DeleteLastMessage()
    {
        Destroy(texts[0]);
        texts.RemoveAt(0);
    }

    public void OnInternalValueChanged()
    {
        string text = inputField.text;
        if (text.EndsWith("\n"))
        {
            string message = text.Remove(text.Length - 1);
            FindObjectOfType<NetworkManager>().SendMessageToServer(message);
            inputField.text = "";
        }
    }
}
