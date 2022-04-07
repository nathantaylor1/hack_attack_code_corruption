using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookInDirectionCode : CodeWithParameters
{

    public override void ExecuteCode()
    {
        var p0 = (GetParameter(0));
        if (!(p0 is null)) {
            // float temp_x = 1;
            Vector2 temp = (Vector2)(object)p0;
            // if (temp.x < 0) temp_x = -1;
            if (Mathf.RoundToInt(Mathf.Sign(module.transform.right.x)) 
                 != Mathf.RoundToInt(Mathf.Sign(temp.x))) {
                module.transform.Rotate(transform.up, 180);
            }
            // module.transform.localScale = new Vector3(temp_x, 1, 1);
        }
        base.ExecuteCode();
    }
}
