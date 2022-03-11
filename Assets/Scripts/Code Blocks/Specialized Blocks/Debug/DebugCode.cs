using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCode : CodeWithParameters
{
    public override void ExecuteCode()
    {
        Debug.Log((string)(object)GetParameter(0));
        base.ExecuteCode();
    }
}
