using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StringCode : Code
{
    //protected string val = "string";

    protected TMP_Text text;

    protected override void Awake()
    {
        base.Awake();
        text = GetComponentInChildren<TMP_Text>();
        //text.text = val;
    }

    public override dynamic ReturnValue()
    {
        return text.text;
    }
}
