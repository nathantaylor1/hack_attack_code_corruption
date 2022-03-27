using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryDrop : MonoBehaviour, IDropHandler
{
    //private GraphicRaycaster gr;
    //private ScrollRect sr;
    private Vector2 canvasPos = Vector2.zero;
    private bool canEdit = false;

    private void Awake()
    {
        //gr = GetComponent<GraphicRaycaster>();
        //sr = GetComponentInParent<ScrollRect>();

        ToggleInteractable(false);
        EventManager.OnToggleEditor.AddListener(ToggleInteractable);
    }

    public void ToggleEnabled(bool enabled)
    {
        canEdit = enabled;
    }

    private void ToggleInteractable(bool enabled)
    {
        if (canEdit)
        {
            //Debug.Log("Toggling code space");
            if (TryGetComponent(out ScrollRect sr))
            {
                /*if (enabled)
                    sr.content.anchoredPosition = canvasPos;
                else
                    canvasPos = sr.content.anchoredPosition;*/
                sr.content.anchoredPosition = canvasPos;
            }
            if (TryGetComponent(out GraphicRaycaster gr))
                gr.enabled = enabled;
        }
        else if (TryGetComponent(out GraphicRaycaster gr)) {
                gr.enabled = enabled;
        }
    }

    public void OnDrop(PointerEventData eventData) {
        if (eventData.pointerDrag.TryGetComponent<Draggable>(out Draggable drag)) {
            if (drag.TryGetComponent(out StartCode sc)) {
                return;
            }
            drag.droppedInto = true;
            eventData.pointerDrag.transform.SetParent(transform, true);
        }
    }
}
