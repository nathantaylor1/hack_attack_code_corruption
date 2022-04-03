using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OppositeDirectionCode : CodeWithParameters
{
    protected Vector2 val = Vector2.zero;

    public override void ExecuteCode()
    {
        Vector2 temp = (Vector2)(object)GetParameter(0);
        val = -temp;
        base.ExecuteCode();
    }

    public override dynamic ReturnValue()
    {
        ExecuteCode();
        return val;
    }
}
