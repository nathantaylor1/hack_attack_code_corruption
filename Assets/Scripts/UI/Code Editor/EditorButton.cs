using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class EditorButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    public Sprite unselectedSprite;
    [SerializeField]
    public Sprite selectedSprite;

    protected Button bn;
    //protected Image img;
    protected CodeEditorSwapper swapper;
    [SerializeField]
    protected Transform window;
    public UnityEvent clicked = new UnityEvent();

    protected void Awake()
    {
        bn = GetComponent<Button>();
        //img = GetComponent<Image>();
        bn.onClick.AddListener(SelectButton);
    }

    public void SetSwapper (CodeEditorSwapper _swapper)
    {
        swapper = _swapper;
    }

    public void Init(CodeEditorSwapper _swapper, Transform _window)
    {
        //Debug.Log("Initialized - button: " + gameObject.name + "; window: " + _window.name);
        swapper = _swapper;
        window = _window;
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        Code code = null;
        if (eventData.pointerDrag != null)
            code = eventData.pointerDrag.GetComponent<Code>();
        if (code != null)
        {
            SelectButton();
        }
    }

    public void SelectButton()
    {
        //Debug.Log("Selected - button: " + gameObject.name + "; window: " + window.name);
        swapper.SetActiveWindow(window, this);
        GetComponent<Image>().sprite = selectedSprite;
        clicked?.Invoke();
    }

    public void DeselectButton()
    {
        GetComponent<Image>().sprite = unselectedSprite;
    }
}
