using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatCode : Code
{
    public float val = 0f;
    protected TMP_Text text;

    protected override void Awake()
    {
        base.Awake();
        text = GetComponentInChildren<TMP_Text>();
        SetText();
    }

    public override dynamic ReturnValue()
    {
        return val;
    }

    public void SetText()
    {
        text.text = val + "x";
    }
}
