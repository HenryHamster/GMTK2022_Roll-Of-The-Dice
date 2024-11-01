using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class HoverInfoScript : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public float mouseFirstOver;
    public float hoverTime;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseFirstOver = Mathf.Infinity;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        mouseFirstOver = Time.timeSinceLevelLoad;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad - mouseFirstOver >= hoverTime)
        {
            gameObject.SetActive(false);
        }
    }
}
