using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoolCode : Code
{
    [SerializeField]
    protected bool val = false;
    /*[SerializeField]
    protected string trueText = "true";
    [SerializeField]
    protected string falseText = "false";*/

    protected TMP_Text text;

    protected override void Awake()
    {
        base.Awake();
        text = GetComponentInChildren<TMP_Text>();
        text.text = val.ToString();
        /*if (val)
        {
            text.text = trueText;
        }
        else
        {
            text.text = falseText;
        }*/
    }

    public override dynamic ReturnValue()
    {
        return val;
    }
}
