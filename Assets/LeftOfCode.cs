using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftOfCode : CodeWithParameters
{
    protected bool val = false;
    public float detection_radius = 5f;

    public override void ExecuteCode()
    {
        Transform trans = module.transform;
        
        int layer = (int)(object)GetParameter(0);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(trans.position.x, trans.position.y), detection_radius, (1 << layer));
        Collider2D closest_collider = module.FindClosestCollider(colliders, trans);
        if(!closest_collider)
        {
            val = false;
            return;
        }

        val = (trans.position.x - closest_collider.transform.position.x) > 1;
    }

    public override dynamic ReturnValue()
    {
        ExecuteCode();
        return val;
    }
}
