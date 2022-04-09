using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OppositeDirectionCode : CodeWithParameters
{
    protected Vector2 val = Vector2.zero;

    public override void ExecuteCode()
    {
        var p0 = GetParameter(0);
        if (!(p0 is null)) {
            Vector2 temp = (Vector2)(object)p0;
            val = -temp;
        }
        base.ExecuteCode();
    }

    public override dynamic ReturnValue()
    {
        ExecuteCode();
        return val;
    }
}
