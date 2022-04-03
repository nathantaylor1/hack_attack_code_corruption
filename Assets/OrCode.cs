using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrCode : CodeWithParameters
{
    protected bool val = false;

    public override void ExecuteCode()
    {
        val = ((bool)(object)GetParameter(0) || (bool)(object)(GetParameter(1)));
        base.ExecuteCode();
    }

    public override dynamic ReturnValue()
    {
        ExecuteCode();
        return val;
    }
}
