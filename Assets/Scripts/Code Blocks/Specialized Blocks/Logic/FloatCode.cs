using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatCode : Code
{
    [SerializeField]
    protected float val = 0f;
    protected TMP_Text text;

    protected override void Awake()
    {
        base.Awake();
        text = GetComponentInChildren<TMP_Text>();
        text.text = val.ToString();
    }

    public override dynamic ReturnValue()
    {
        return val;
    }
}
