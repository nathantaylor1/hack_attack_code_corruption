using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDirectionCode : Code
{
    [SerializeField]
    protected Vector2 val = new Vector2(0, 0);
    // Start is called before the first frame update

    public override void ExecuteCode()
    {
        Vector3 temp = module.transform.localScale;

        // If spawned object
        if(module.father != null)
        {
            temp = module.father.transform.localScale;
        }

        //print(temp);
        val.x = temp.x;
        base.ExecuteCode();
    }

    public override dynamic ReturnValue()
    {
        ExecuteCode();
        //print(val);
        return val;
    }
}
