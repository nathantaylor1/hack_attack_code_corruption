using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableFloat : CollectableBlock
{
    public float value;
    public override void SetValue(GameObject go)
    {
        FloatCode fc = go.GetComponent<FloatCode>();
        fc.val = value;
        fc.SetText();
    }
}
