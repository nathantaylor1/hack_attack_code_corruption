using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DirectionCode : Code
{
    [SerializeField]
    protected Vector2 val;
    [SerializeField]
    protected Sprite arrowIcon;
    [SerializeField]
    protected Image arrowImage;

    protected TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        base.Awake();
        text = GetComponentInChildren<TMP_Text>();

        arrowImage.sprite = arrowIcon;
        Quaternion quat = new Quaternion();
        quat.SetFromToRotation(Vector2.up, val);
        arrowImage.transform.rotation *= quat;
        /*if(val.x == 1)
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
        }*/

    }

    public override dynamic ReturnValue()
    {
        return val;
    }
}
