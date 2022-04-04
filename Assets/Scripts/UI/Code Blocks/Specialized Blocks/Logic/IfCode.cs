using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfCode : CodeWithBodies
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
            GetBody(0).SetModule(module);
            GetBody(0).StopExecution();
        }
        base.ExecuteCode();
    }

    public override void StopExecution()
    {
        GetBody(0).SetModule(module);
        GetBody(0).StopExecution();
        base.ExecuteCode();
    }
}
