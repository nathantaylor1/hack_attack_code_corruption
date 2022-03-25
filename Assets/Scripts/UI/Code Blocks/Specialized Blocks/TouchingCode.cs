using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingCode : Code
{
    // Start is called before the first frame update
    private bool val;
    protected override void Awake()
    {
        base.Awake();
        // TODO: change to script's collider
        // col = GameManager.instance.player.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    // void FixedUpdate()
    // {
    // }

    public override void ExecuteCode()
    {
        if(module.col.IsTouchingLayers(~module.col.gameObject.layer))
        {
            val = true;
        }
        else
        {
            val = false;
        }
        module.OnCheckCollision?.Invoke();
        base.ExecuteCode();
    }

    public override dynamic ReturnValue()
    {
        ExecuteCode();
        return val;
    }
}
