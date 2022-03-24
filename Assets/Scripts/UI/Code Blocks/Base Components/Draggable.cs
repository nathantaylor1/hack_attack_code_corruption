using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {
    protected Canvas canvas;
    protected RectTransform rect;
    protected CanvasGroup canvasGroup;
    public Vector3 locationBeforeDrag;
    protected Transform originalParent;
    public bool droppedInto = false;

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
        // transform.SetParent(canvas.transform);
        // transform.SetAsLastSibling();
    }

    public virtual void OnBeginDrag(PointerEventData eventData) {
        //Debug.Log("OnBeginDrag");
        UnClip();
        canvasGroup.alpha = .6f;
        locationBeforeDrag = rect.anchoredPosition3D;
        // must remove parent in order to show thing being dragged on top
        originalParent = transform.parent;
        transform.SetParent(EditorController.instance.editor_screen.transform);

        canvasGroup.blocksRaycasts = false;
    }

    public virtual void OnDrag(PointerEventData eventData) {
        // Debug.Log("OnDrag");
        rect.anchoredPosition += eventData.delta / EditorController.instance.editor_screen.scaleFactor;
    }

    public virtual void OnEndDrag(PointerEventData eventData) {
        // Debug.Log(rect.localPosition);
        if ( !droppedInto && !transform.parent.TryGetComponent<Code>(out Code testCode) 
                && !transform.parent.TryGetComponent<InputField>(out InputField testField)) {
            transform.SetParent(originalParent);
            // Debug.Log(rect.localPosition);
            RectTransform canvasRect = canvas.transform as RectTransform;
            var rectMiddle = rect.localPosition + Vector3.down * rect.rect.height / 2 + Vector3.right * rect.rect.width / 2;
            if (rectMiddle.y > canvasRect.rect.yMax || rectMiddle.y < canvasRect.rect.yMin ||
                rectMiddle.x > canvasRect.rect.xMax || rectMiddle.x < canvasRect.rect.xMin)
            {
                rect.anchoredPosition3D = locationBeforeDrag;
            }
        }
        droppedInto = false;
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