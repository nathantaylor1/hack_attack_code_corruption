using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        blockInputField = transform.parent.GetComponentInParent<InputField>();
        if (blockInputField == null)
            Debug.Log("No input field found");
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        Code code = null;
        if (eventData.pointerDrag != null)
            code = eventData.pointerDrag.GetComponent<Code>();
        if (code != null && (blockInputField.CanAcceptInput(code.ReturnType) || 
            (blockInputField.GetCode() != null && code.GetInstanceID() == blockInputField.GetCode().GetInstanceID())))
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
        if (code != null && (blockInputField.CanAcceptInput(code.ReturnType) ||
            (blockInputField.GetCode() != null && code.GetInstanceID() == blockInputField.GetCode().GetInstanceID())))
        { 
            /*if (dropMethod == DropMethod.underneath)
            {
                if (eventData.pointerDrag.TryGetComponent(out LayoutElement le))
                {
                    le.ignoreLayout = true;
                }
                if (blockInputField.gameObject.TryGetComponent(out RectTransform rt))
                {
                    dropPos = -rt.rect.size * Vector2.up;
                }
            }*/

            code.transform.SetParent(blockInputField.transform, false);
            resize(code);
        }

    }
    public void resize(Code code) {
        if (blockInputField.gameObject.TryGetComponent(out BlockResizer br))
        {
            br.UpdateSize();
        }
        blockInputField.AddInputBlock(code);
    }

    public virtual void RemoveBlock()
    {
        blockInputField.RemoveInputBlock();
    }
}