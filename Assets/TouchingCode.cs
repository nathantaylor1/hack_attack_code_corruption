using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingCode : Code
{
    // Start is called before the first frame update
    private bool val;
    protected Collider2D col;
    protected override void Awake()
    {
        base.Awake();
        // TODO: change to script's collider
        col = GameManager.instance.player.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(col.IsTouchingLayers(~col.gameObject.layer))
        {
            val = true;
        }
        else
        {
            val = false;
        }
    }

    public override dynamic ReturnValue()
    {
        return val;
    }
}
