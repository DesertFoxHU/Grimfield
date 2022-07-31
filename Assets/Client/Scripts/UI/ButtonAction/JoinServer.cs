using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class JoinServer : MonoBehaviour
{
    public TextMeshProUGUI ipAddressField;
    public TextMeshProUGUI nameField;

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        if (ipAddressField.text == "" || ipAddressField.text == null) return;
        if (nameField.text == "" || nameField.text == null) return;

        FindObjectOfType<NetworkManager>().Name = nameField.text;
        //FindObjectOfType<NetworkManager>().Connect(ipAddressField.text);
        FindObjectOfType<NetworkManager>().Connect();
    }
}
