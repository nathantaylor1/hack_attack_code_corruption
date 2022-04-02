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
    protected Transform window = null;
    protected CodeModule cm = null;
    private bool invisible = false;
    private bool selected = false;
    private Vector2 size;
    public UnityEvent clicked = new UnityEvent();

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

    // Only call this with enemies
    public void SetModule(CodeModule _cm) {
        cm = _cm;
        if (cm.TryGetComponent(out EnemyID eid)) {
            eid.rewind.AddListener(Show);
            cm.died.AddListener(Hide);
        }
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
        if (window == null) {
            return;
        }
        selected = true;
        swapper.SetActiveWindow(window, this);
        GetComponent<Image>().sprite = selectedSprite;
        clicked.Invoke();
    }

    public void DeselectButton()
    {
        selected = false;
        GetComponent<Image>().sprite = unselectedSprite;
    }

    public void Hide() {
        window.SetAsFirstSibling();
        if (selected) {
            DeselectButton();
            swapper.SelectPlayerWindow();
        }
        gameObject.SetActive(false);
        Debug.Log("hidden button");
    }

    public void Show() {
        gameObject.SetActive(true);
    }
}
