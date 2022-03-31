using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EditorButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    public Sprite unselectedSprite;
    [SerializeField]
    public Sprite selectedSprite;

    protected Button bn;
    //protected Image img;
    protected CodeEditorSwapper swapper;
    protected Transform window;

    protected void Awake()
    {
        bn = GetComponent<Button>();
        //img = GetComponent<Image>();
        DeselectButton();
        bn.onClick.AddListener(SelectButton);
    }

    public void Init(CodeEditorSwapper _swapper, Transform _window)
    {
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
        swapper.SetActiveWindow(window, this);
        GetComponent<Image>().sprite = selectedSprite;
    }

    public void DeselectButton()
    {
        GetComponent<Image>().sprite = unselectedSprite;
    }
}
