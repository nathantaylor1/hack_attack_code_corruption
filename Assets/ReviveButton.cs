using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
    protected bool showing = false;

    protected void Awake()
    {
        bn = GetComponent<Button>();
        img = GetComponent<Image>();
        ew = GetComponentInParent<EditorWindow>();
        bn.onClick.AddListener(ReviveModule);
    }

    protected void Update()
    {
        if (ew.GetModule() && ew.GetModule().hackable && !showing) {
            GetComponentInChildren<TMP_Text>().text = "REVIVE";
            showing = true;
            img.color = Color.red;
        }
        if(!isFlashing && showing) StartCoroutine(Flash());
    }

    public void ReviveModule()
    {
        if (ew != null && showing)
        {
            StopCoroutine(Flash());
            ew.ToggleCanExecute(true);
            ew.GetModule().hackable = false;
            ew.GetModule().GetComponent<HasHealth>().Revive();
            isFlashing = true;
            showing = false;
            GetComponentInChildren<TMP_Text>().text = "REVIVED";
            img.color = Color.green;
            EditorController.instance.SafeClose();
        }
    }

    protected IEnumerator Flash()
    {
        isFlashing = true;
        Color tempColor = img.color;
        tempColor.a = .5f;
        if (showing) {
            img.color = tempColor;
        }
        yield return new WaitForSecondsRealtime(.75f);
        tempColor.a = 1f;
        if (showing) {
            img.color = tempColor;
        }
        yield return new WaitForSecondsRealtime(.75f);
        isFlashing = false;
    }

}
