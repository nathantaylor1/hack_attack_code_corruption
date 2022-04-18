using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ReviveButton : MonoBehaviour
{
    [SerializeField]
    public Sprite unselectedSprite;
    [SerializeField]
    public Sprite selectedSprite;

    protected Button bn;
    protected EditorWindow ew;
    protected Image img;
    protected bool isFlashing = false;

    protected void Awake()
    {
        bn = GetComponent<Button>();
        img = GetComponent<Image>();
        ew = GetComponentInParent<EditorWindow>();
        bn.onClick.AddListener(ReviveModule);
    }

    protected void Update()
    {
        if(!isFlashing) StartCoroutine(Flash());
    }

    public void ReviveModule()
    {
        if (ew != null)
        {
            ew.ToggleCanExecute(true);
            ew.GetModule().hackable = false;
            ew.GetModule().GetComponent<HasHealth>().Revive();
            EditorController.instance.SafeClose();
        }
    }

    protected IEnumerator Flash()
    {
        isFlashing = true;
        Color tempColor = img.color;
        tempColor.a = .5f;
        img.color = tempColor;
        yield return new WaitForSecondsRealtime(.75f);
        tempColor.a = 1f;
        img.color = tempColor;
        yield return new WaitForSecondsRealtime(.75f);
        isFlashing = false;
    }

}
