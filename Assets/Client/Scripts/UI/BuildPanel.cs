using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BuildPanel : MonoBehaviour
{
    public enum Category
    {
        Village,
        Tower,
        Factory
    }

    public bool isOpen;
    public GameObject categoryButtonHolder;
    public Category category = Category.Village;

    public void Trigger()
    {
        isOpen = !isOpen;
        GetComponent<Animator>().SetTrigger("Toggle");
    }

    public void HideCategoryButtons()
    {
        categoryButtonHolder.SetActive(false);
    }

    public void ShowCategoryButtons()
    {
        categoryButtonHolder.SetActive(true);
    }
}
