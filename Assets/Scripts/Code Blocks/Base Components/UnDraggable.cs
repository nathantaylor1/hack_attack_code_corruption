using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnDraggable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler
{
    private Image image;

    private void Awake()
    {
        image = gameObject.GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
        {
            image.raycastTarget = false;
            //eventData.pointerDrag = null;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.raycastTarget = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        eventData.pointerDrag = null;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //image.raycastTarget = true;
    }
}
