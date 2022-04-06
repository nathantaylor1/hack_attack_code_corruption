using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeesCode : CodeWithParameters
{
    public float boxSize = 5f;
    private bool val;
    
    public override void ExecuteCode()
    {
        Bounds bds = module.col.bounds;
        Transform tf = module.transform;
        LayerMask elm = 1 << (int)(object)GetParameter(0);

        Vector2 size = new Vector2(boxSize, boxSize);
        val = Physics2D.OverlapBox(tf.position, size, 0f, elm);

        base.ExecuteCode();
    }

    public override dynamic ReturnValue()
    {
        ExecuteCode();
        return val;
    }
}
