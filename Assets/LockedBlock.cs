using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockedBlock : MonoBehaviour
{
    [SerializeField] bool canBeRemoved = false;
    [SerializeField] float lockedAlphaValue = .5f;
    [SerializeField] GameObject lockIcon;

    private void Awake()
    {
        if (canBeRemoved) return;

        lockIcon.SetActive(true);

        if (TryGetComponent(out Draggable drag)) drag.enabled = false;
        if (TryGetComponent(out UnDraggable undrag)) undrag.enabled = false;

        Transform background = transform.Find("Background");
        SetAlpha(background);

        Transform selection = background.Find("Selection Image");
        SetAlpha(selection);
    }


    void SetAlpha(Transform trans)
    {
        if (!trans) return;

        if(trans.TryGetComponent(out Image img))
        {
            Color tempColor = img.color;
            tempColor.a = lockedAlphaValue;
            img.color = tempColor;
        }
    }
}
