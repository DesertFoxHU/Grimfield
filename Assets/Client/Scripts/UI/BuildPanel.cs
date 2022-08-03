using System;
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
    [HideInInspector] public Category category = Category.Village;
    public GameObject villageCategory;
    public GameObject towerCategory;
    public GameObject factoryCategory;
    public GameObject segmentPrefab;

    public void Start()
    {
        foreach (BuildMenuElement element in BuildMenuElementRegistry.elements)
        {
            int count = GetCategorysObject(element.Category).transform.childCount;
            float Y = 0 + (-170 * count);

            GameObject newSegment = Instantiate(segmentPrefab, new Vector3(0f, Y, 0f), Quaternion.identity);
            newSegment.transform.SetParent(GetCategorysObject(element.Category).transform, false);
            newSegment.GetComponent<BuildMenuSegment>().Load(element);
        }
        LoadCategory(Category.Village);
    }

    public void LoadCategory(Category category)
    {
        this.category = category;
        villageCategory.SetActive(false);
        towerCategory.SetActive(false);
        factoryCategory.SetActive(false);

        GetCategorysObject(category).SetActive(true);
    }

    public void LoadCategory(string categoryRaw)
    {
        LoadCategory((Category)Enum.Parse(typeof(Category), categoryRaw));
    }

    public GameObject GetCategorysObject(Category category)
    {
        if (category == Category.Village) return villageCategory;
        else if (category == Category.Tower) return towerCategory;
        else return factoryCategory;
    }

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
