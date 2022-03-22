using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DirectionCode : Code
{
    [SerializeField]
    protected Vector2 val;

    protected TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        base.Awake();
        text = GetComponentInChildren<TMP_Text>();
        
        if(val.x == 1)
        {
            text.text = "-->";
        }
        else if(val.x == -1)
        {
            text.text = "<--";
        }
        else if(val.y == 1)
        {
            text.text = "^";
        }
        else
        {
            text.text = "v";
        }
    }

    public override dynamic ReturnValue()
    {
        return val;
    }
}
