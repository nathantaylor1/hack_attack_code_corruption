using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropInto : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler {
    protected Canvas canvas;
    protected InputField blockInputField;
    protected BlockResizer blockResizer;
    protected RectTransform rect;
    protected enum DropMethod { inside, underneath }
    [SerializeField]
    [Tooltip("Do blocks dropped here go inside the parent block or underneath it?")]
    protected DropMethod dropMethod = DropMethod.inside;
    protected Vector2 dropPos = Vector2.zero;

    protected virtual void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        rect = GetComponent<RectTransform>();
        blockInputField = transform.parent.GetComponent<InputField>();
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        Code code = null;
        if (eventData.pointerDrag != null)
            code = eventData.pointerDrag.GetComponent<Code>();
        if (code != null && blockInputField.CanAcceptInput(code.ReturnType) 
            && eventData.pointerDrag.transform != transform.parent)
        {
            blockInputField.Select();
        }
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            blockInputField.Deselect();
        }
    }

    public virtual void OnDrop(PointerEventData eventData) {
        Code code = null;
        if (eventData.pointerDrag != null)
            code = eventData.pointerDrag.GetComponent<Code>();
        if (code != null && blockInputField.CanAcceptInput(code.ReturnType)
            && eventData.pointerDrag.transform != transform.parent) {
            if (dropMethod == DropMethod.inside)
            {
                if (transform.parent.TryGetComponent(out BlockResizer br))
                {
                    br.UpdateSize();
                }
            }
            else
            {
                dropPos = rect.rect.size * Vector2.up;
                eventData.pointerDrag.transform.position = dropPos;
                if (eventData.pointerDrag.TryGetComponent(out LayoutElement le))
                {
                    //le.ignoreLayout = true;
                }

            }
            eventData.pointerDrag.transform.SetParent(transform.parent, false);

            blockInputField.AddInputBlock(code);
        }
    }

    public virtual void RemoveBlock(Vector2 size)
    {
        blockInputField.RemoveInputBlock();
    }

    /*public virtual void moveDown(float y) {
        bottom.anchoredPosition += Vector2.down * y;
        middle.sizeDelta += Vector2.up * y;
        middle.anchoredPosition += Vector2.down * y / 2f;
        // rect.sizeDelta += Vector2.up * y;
        // rect.anchoredPosition += Vector2.down * y / 2f;
        dropPos += Vector2.down * y;
    }

    public virtual void moveUp(float y) {
        bottom.anchoredPosition += Vector2.up * y;
        middle.sizeDelta += Vector2.down * y;
        middle.anchoredPosition += Vector2.up * y / 2f;
        // rect.sizeDelta += Vector2.down * y;
        // rect.anchoredPosition += Vector2.up * y / 2f;
        dropPos += Vector2.up * y;
    }

    public virtual void shiftUp(float y, RectTransform removed) {
        moveUp(y);
        bool test = false;
        foreach (RectTransform child in transform) {
            if (test) {
                if (child.TryGetComponent(out Draggable drag)) {
                    child.anchoredPosition += Vector2.up * y;
                }
            }
            if (removed == child) {
                test = true;
            }
        }
        if (transform.parent.TryGetComponent(out DropInto drop)) {
            drop.shiftUp(y, transform as RectTransform);
        }
    }


    public virtual void shiftDown(float y, RectTransform added) {
        moveDown(y);
        bool test = false;
        foreach (RectTransform child in transform) {
            if (test) {
                if (child.TryGetComponent(out Draggable drag)) {
                    child.anchoredPosition += Vector2.down * y;
                }
            }
            if (added == child) {
                test = true;
            }
        }
        if (transform.parent.TryGetComponent(out DropInto drop)) {
            drop.shiftDown(y, transform as RectTransform);
        }
    }*/

    // TODO
    // if draggable is close enough show outline either in or under
    // and make sure outline is either element or wrapper template
    // add a get height function when removing and adding
}