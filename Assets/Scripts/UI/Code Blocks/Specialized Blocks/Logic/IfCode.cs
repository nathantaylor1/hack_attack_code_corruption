using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfCode : CodeWithBodies
{
    // used so we only call stop execution one time
    protected bool wasJustTrue = false;
    public override void ExecuteCode()
    {
        if ((bool)(object)GetParameter(0, module))
        {
            wasJustTrue = true;
            GetBody(0).SetModule(module);
            GetBody(0).ExecuteCode();
        }
        else if (wasJustTrue)
        {
            wasJustTrue = false;
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
