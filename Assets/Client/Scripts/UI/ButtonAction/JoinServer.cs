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
        if (ipAddressField.text == "" || ipAddressField.text == null)
        {
            FindObjectOfType<MessageDisplayer>().SetMessage("IpAddress field cannot be empty");
            return;
        }
        if (nameField.text == "" || nameField.text == null)
        {
            FindObjectOfType<MessageDisplayer>().SetMessage("Name field cannot be empty");
            return;
        }

        if(nameField.text.Length > 16)
        {
            FindObjectOfType<MessageDisplayer>().SetMessage("Name cannot be longer than 16 characters!");
            return;
        }

        FindObjectOfType<NetworkManager>().Name = nameField.text;
        //FindObjectOfType<NetworkManager>().Connect(ipAddressField.text);
        FindObjectOfType<NetworkManager>().Connect();
    }
}
