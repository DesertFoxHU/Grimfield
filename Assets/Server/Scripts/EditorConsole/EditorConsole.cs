using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EditorConsole : MonoBehaviour
{
    public KeyCode toggle;
    public GameObject panel;
    public TMP_InputField input;
    public List<TextMeshProUGUI> textSlots;

    public void Update()
    {
        if (Input.GetKeyDown(toggle))
        {
            panel.SetActive(!panel.activeSelf);
        }
    }

    public void OnInputChanged()
    {
        string text = input.text;
        if (text.EndsWith("\n"))
        {
            string command = text.Remove(text.Length - 1);
            //Write(command.Trim());
            WriteMultiline(ServerSide.NetworkManager.Instance.HandleCommand(command.Trim()).value);
            input.text = "";
        }
    }

    public void WriteMultiline(string text)
    {
        foreach (string textPart in text.Split('\n'))
        {
            Write(textPart);
        }
    }

    public void Write(string text)
    {
        if (String.IsNullOrEmpty(text)) return;

        string previousText = textSlots[0].text;
        for(int i = 1; i < textSlots.Count; i++)
        {
            string temp = textSlots[i].text;
            textSlots[i].text = previousText;
            previousText = temp;
        }

        textSlots[0].text = $"[{DateTime.Now.ToString("HH:mm:ss")}] " + text;
    }
}
