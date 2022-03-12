using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {
    protected Canvas canvas;
    protected RectTransform rect;
    protected CanvasGroup canvasGroup;

    protected virtual void Awake() {
        canvas = GetComponentInParent<Canvas>();
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void UnClip() {
        if (transform.parent.GetComponent<InputField>() != null)
        {
            DropInto dropHandler = transform.parent.GetComponentInChildren<DropInto>();
            if (dropHandler != null)
            {
                dropHandler.RemoveBlock(rect.rect.size);
            }
        }
        if (transform.parent.TryGetComponent(out BlockResizer br))
        {
            br.UpdateSize();
        }
        /*if (transform.TryGetComponent(out LayoutElement le))
        {
            le.ignoreLayout = false;
        }*/
        transform.SetParent(canvas.transform);
        transform.SetAsLastSibling();
    }

    public virtual void OnBeginDrag(PointerEventData eventData) {
        //Debug.Log("OnBeginDrag");
        UnClip();
        canvasGroup.alpha = .6f;
        // RectTransformUtility.ScreenPointToLocalPointInRectangle (transform.parent.transform as RectTransform, eventData.position, null, out Vector2 localPoint);
        // rect.anchoredPosition = localPoint;
        canvasGroup.blocksRaycasts = false;
    }

    public virtual void OnDrag(PointerEventData eventData) {
        // Debug.Log("OnDrag");
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public virtual void OnEndDrag(PointerEventData eventData) {
        // Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public virtual void OnPointerDown(PointerEventData eventData) {
        // Debug.Log("OnPointerDown");
        canvasGroup.alpha = 0.6f;
    }

    public virtual void OnPointerUp(PointerEventData eventData) {
        // Debug.Log("OnPointerDown");
        canvasGroup.alpha = 1f;
    }
}