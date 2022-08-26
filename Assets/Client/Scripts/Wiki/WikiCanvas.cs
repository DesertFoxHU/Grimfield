using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WikiCanvas : MonoBehaviour
{
    public GameObject panel;
    public List<OptionalConnectedPage> pages;
    private OptionalConnectedPage? current;

    private int index = 0;
    public int pageIndex
    {
        get { return index; }
        set
        {
            if(value < 0)
            {
                value = 0;
            }
            else if(value >= pages.Count)
            {
                value = pages.Count - 1;
            }
            index = value;
        }
    }

    private void Start()
    {
        LoadCurrent();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            panel.SetActive(!panel.activeSelf);
        }
    }

    public void LoadCurrent()
    {
        if(current != null)
        {
            current.GetValueOrDefault().SetActive(false);
        }

        current = pages[pageIndex];
        current.GetValueOrDefault().SetActive(true);
    }

    public void Previous()
    {
        pageIndex -= 1;
        LoadCurrent();
    }

    public void Next()
    {
        pageIndex += 1;
        LoadCurrent();
    }
}

[System.Serializable]
public struct OptionalConnectedPage
{
    public WikiPage page1;
    public WikiPage page2;

    public void SetActive(bool isActive)
    {
        if(page1 != null) page1.gameObject.SetActive(isActive);
        if(page2 != null) page2.gameObject.SetActive(isActive);
    }
}
