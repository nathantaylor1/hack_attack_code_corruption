using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntCode : Code
{
    public int val = 0;
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
        text.text = val.ToString();
    }
}
