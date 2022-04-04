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

    protected void Awake()
    {
        bn = GetComponent<Button>();
        //img = GetComponent<Image>();
        ew = GetComponentInParent<EditorWindow>();
        bn.onClick.AddListener(ReviveModule);
    }

    public void ReviveModule()
    {
        if (ew != null)
        {
            ew.ToggleCanExecute(true);
            ew.GetModule().hackable = false;
            EditorController.instance.SafeClose();
        }
    }
}
