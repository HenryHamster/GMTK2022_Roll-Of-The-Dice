using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SelfPauseMenuScript : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public CanvasGroup selfCanvasGroup;
    public OptionsMenuScript oms;
    public bool mouseOver;
    public RectTransform selfTransform;

    private void Update()
    {
        if (!selfCanvasGroup.interactable && (Input.GetKeyDown(KeyCode.Escape)||Input.GetKeyDown(KeyCode.Backspace))&&!oms.winCanvasGroup.blocksRaycasts&&!GlobalEventsManager.instance.paused)
        {
            oms.openPauseMenu();
        }
        else if (selfCanvasGroup.interactable && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))&& !oms.winCanvasGroup.blocksRaycasts)
        {
            oms.closePauseMenu();
        }
        if (selfCanvasGroup.interactable && Input.GetMouseButtonDown(0)&& !mouseOver&& !oms.winCanvasGroup.blocksRaycasts)
        {
            oms.closePauseMenu();
        }

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("MouseEntered");
        mouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            if (eventData.pointerCurrentRaycast.gameObject.transform.parent != null && eventData.pointerCurrentRaycast.gameObject.transform.parent == transform||(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent!=null&&eventData.pointerCurrentRaycast.gameObject.transform.parent.parent==transform))
            {
                return;
            }
        }
        //Debug.Log(eventData.selectedObject.name);
        /*if (eventData.selectedObject.transform.parent==transform)
        {
            return;
        }*/
        Debug.Log("MouseExited");
        mouseOver = false;
    }
   
}
