using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorButton : MonoBehaviour
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
        bn.onClick.AddListener(SelectButton);
    }

    public void Init(CodeEditorSwapper _swapper, Transform _window)
    {
        swapper = _swapper;
        window = _window;
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
