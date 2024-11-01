using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public GameObject hoverInfo;
    public float mouseFirstOver;
    public float hoverTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseFirstOver = Time.timeSinceLevelLoad;
        Debug.Log("MouseEntered");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        mouseFirstOver =Mathf.Infinity;
        Debug.Log("Mouse Exited");
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad - mouseFirstOver>=hoverTime)
        {
            //hoverInfo.SetActive(true);
            mouseFirstOver = Mathf.Infinity;
        }
    }
}
