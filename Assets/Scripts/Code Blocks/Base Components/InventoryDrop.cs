using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryDrop : MonoBehaviour, IDropHandler
{
    // Start is called before the first frame update
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
