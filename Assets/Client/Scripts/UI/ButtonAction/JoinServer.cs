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
        if (PlayerPrefs.HasKey("IpAddress"))
        {
            ipAddressField.text = PlayerPrefs.GetString("IpAddress");
        }
        if (PlayerPrefs.HasKey("Name"))
        {
            nameField.text = PlayerPrefs.GetString("Name");
        }
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

        NetworkManager.Instance.Name = nameField.text;

        string ipAddress = ipAddressField.text;
        if (ipAddress.Contains(':'))
        {
            /*ushort port;
            ushort.TryParse(ipAddress.Split(':')[1], out port);
            NetworkManager.Instance.port = port;*/
            Debug.Log($"{ipAddress} port: {ipAddress.Split(':')[1]}");
            NetworkManager.Instance.port = ushort.Parse(ipAddress.Split(':')[1]);
            NetworkManager.Instance.ip = ipAddress.Split(':')[0];
        }
        else
        {
            NetworkManager.Instance.ip = ipAddress;
        }

        PlayerPrefs.SetString("IpAddress", ipAddress);
        PlayerPrefs.SetString("Name", NetworkManager.Instance.Name);
        PlayerPrefs.Save();

        FindObjectOfType<NetworkManager>().Connect();
    }
}
