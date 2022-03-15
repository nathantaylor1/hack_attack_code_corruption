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
}
