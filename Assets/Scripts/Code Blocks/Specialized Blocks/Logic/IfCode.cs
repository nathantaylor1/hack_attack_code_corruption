using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfCode : CodeWithBodies
{
    public override void ExecuteCode()
    {
        if ((bool)(object)GetParameter(0))
        {
            GetBody(0).ExecuteCode();
        }
        base.ExecuteCode();
    }

    public override void Continue()
    {
        if (GetNext() != null)
            GetNext().ExecuteCode();
        else
        {
            Code prevBlock = transform.parent.GetComponentInParent<Code>();
            if (prevBlock != null)
            {
                prevBlock.SignalCompletion();
            }
        }
    }

    public override void SignalCompletion()
    {
        Continue();
    }
}
