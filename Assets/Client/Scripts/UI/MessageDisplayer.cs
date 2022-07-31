using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageDisplayer : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI messageText;
    private bool isActive = false;
    private double remainTime;

    public void SetMessage(string message)
    {
        panel.SetActive(true);
        messageText.text = message;

        isActive = true;
        remainTime = 3;
    }

    void Update()
    {
        if (!isActive) return;

        remainTime -= Time.deltaTime;
        if(remainTime < 0)
        {
            messageText.text = "";
            panel.SetActive(false);
            isActive = false;
        }
    }
}
