using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookInDirectionCode : CodeWithParameters
{

    public override void ExecuteCode()
    {
        float temp_x = 1;
        Vector2 temp = (Vector2)(object)(GetParameter(0));
        if (temp.x < 0) temp_x = -1;

        module.transform.localScale = new Vector3(temp_x, 1, 1);
        base.ExecuteCode();
    }
}
