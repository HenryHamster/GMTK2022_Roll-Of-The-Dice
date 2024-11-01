using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SelfOptionMenuScript : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public CanvasGroup selfCanvasGroup;
    public OptionsMenuScript oms;
    public bool mouseOver;
    private void Update()
    {
        if (selfCanvasGroup.interactable&&(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace)))
        {
            oms.closeOptionMenu();
        }

        if (selfCanvasGroup.interactable && Input.GetMouseButtonDown(0) && !mouseOver)
        {
            oms.closeOptionMenu();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            Transform cTransform = eventData.pointerCurrentRaycast.gameObject.transform;
            while (cTransform != null)
            {
                cTransform = cTransform.parent;
                if (cTransform == transform)
                {
                    return;
                }
            }
        }
        mouseOver = false;
    }
}
