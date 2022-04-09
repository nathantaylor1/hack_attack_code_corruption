using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowardsCode : CodeWithParameters
{
    private Vector2 val = new Vector2(0, 0);
    public float detection_radius = 5f;
    [SerializeField] bool is_towards;
    // Start is called before the first frame update
    public override void ExecuteCode()
    {

        var p0 = GetParameter(0);
        if (p0 is null) {
            base.ExecuteCode();
            return;
        }

        Transform trans = module.transform;
        int layer = (int)(object)p0;
        //print("layer is " + layer);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(trans.position.x, trans.position.y), detection_radius, (1 << layer));
        Collider2D closest_collider = module.FindClosestCollider(colliders, trans);

        if (!closest_collider)
        {
            val = Vector2.zero;
            return;
        }

        if(closest_collider.GetComponent<Renderer>().isVisible && trans.GetComponent<Renderer>().isVisible)
        {
            if(is_towards)
            {
                val = closest_collider.transform.position - trans.position;
            }
            else
            {
                val = trans.position - closest_collider.transform.position;
            }
            val = val.normalized;
        }
        else
        {
            val = Vector2.zero;
        }

        base.ExecuteCode();
    }

    public override dynamic ReturnValue()
    {
        ExecuteCode();
        //print("val: " + val);
        //Debug.Log("Cursor direction: " + (Vector2)(object)val);
        return val;
    }
}
