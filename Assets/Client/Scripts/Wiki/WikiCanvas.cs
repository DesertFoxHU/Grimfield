using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WikiCanvas : MonoBehaviour
{
    public GameObject panel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            panel.SetActive(!panel.activeSelf);
        }
    }
}
