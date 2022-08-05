using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Utils
{
    ///
    /// <returns>Returns true if the mouse pointer is above an UI element</returns>
    public static bool IsPointerOverUIElement()
    {
        for (int index = 0; index < GetEventSystemRaycastResults().Count; index++)
        {
            RaycastResult curRaysastResult = GetEventSystemRaycastResults()[index];
            if (curRaysastResult.gameObject.layer == LayerMask.NameToLayer("UI"))
                return true;
        }
        return false;
    }

    private static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }

    public static bool Roll(double chance)
    {
        return Random.Range(0f, 100f) <= chance;
    }

    public static bool RollChance(this double chance)
    {
        return Roll(chance);
    }

    public static void Shuffle<T>(this IList<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
