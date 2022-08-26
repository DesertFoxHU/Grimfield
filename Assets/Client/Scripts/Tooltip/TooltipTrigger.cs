using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Coroutine delay;
    public string title;
    public string content;

    public void OnPointerEnter(PointerEventData eventData)
    {
        delay = StartCoroutine(InternalShow());
    }

    private IEnumerator InternalShow()
    {
        yield return new WaitForSeconds(0.5f);
        TooltipSystem.Show(title, content);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopCoroutine(delay);
        TooltipSystem.Hide();
    }
}
