using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfElseCode : CodeWithBodies
{
    public override void ExecuteCode()
    {
        if ((bool)(object)GetParameter(0, module))
        {
            GetBody(0).SetModule(module);
            GetBody(0).ExecuteCode();
        }
        else
        {
            GetBody(1).SetModule(module);
            GetBody(1).ExecuteCode();
        }
        base.ExecuteCode();
    }

}
