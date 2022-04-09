using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDirectionCode : Code
{
    protected Vector2 val = new Vector2(0, 0);
    // Start is called before the first frame update

    public override void ExecuteCode()
    {
        float x = module.transform.right.x;

        // If spawned object
        if(module.father != null)
        {
            x = module.father.transform.right.x;
        }

        //print(temp);
        val.x = x;
        base.ExecuteCode();
    }

    public override dynamic ReturnValue()
    {
        ExecuteCode();
        //print(val);
        return val;
    }
}
