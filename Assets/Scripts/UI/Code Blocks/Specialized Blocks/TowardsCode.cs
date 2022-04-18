using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowardsCode : CodeWithParameters
{
    private Vector2 val = new Vector2(0, 0);
    public float detection_radius = 5f;
    protected int layer = 0;
    protected bool isActive = false;
    protected Collider2D closestCollider = null;
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
        layer = (int)(object)p0;
        //print("layer is " + layer);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(trans.position.x, trans.position.y), detection_radius, (1 << layer));
        closestCollider = module.FindClosestCollider(colliders, trans);

        if (!closestCollider)
        {
            if (module != null && module.gameObject.layer == 6 && layer == 10)
                CrosshairController.instance.Hide();
            val = Vector2.zero;
            return;
        }
        else
        {
            if (module != null && module.gameObject.layer == 6 && layer == 10)
                CrosshairController.instance.SetTarget(closestCollider.transform);
        }

        /*if(closestCollider.GetComponent<Renderer>().isVisible && trans.GetComponent<Renderer>().isVisible)
        {*/
            if(is_towards)
            {
                val = closestCollider.transform.position - trans.position;
            }
            else
            {
                val = trans.position - closestCollider.transform.position;
            }
            val = val.normalized;
        /*}
        else
        {
            val = Vector2.zero;
        }*/

        base.ExecuteCode();
    }

    public bool CheckIsActive()
    {
        StartCode sc = GetComponentInParent<StartCode>();
        isActive = sc != null;
        return isActive;
    }

    protected int GetLayer()
    {
        var p0 = GetParameter(0);
        if (p0 is null) return 0;
        return (int)(object)p0;
    }

    public override void ExecuteSecondaryCode()
    {
        // If the module is the player & the layer is enemies
        if (module != null && module.gameObject.layer == 6 && GetLayer() == 10)
        {
            ExecuteCode();
        }
        //Debug.Log(module);
        base.ExecuteSecondaryCode();
    }

    public override void StopSecondaryExecution()
    {
        //Debug.Log(module);
        if (module != null && module.gameObject.layer == 6 && layer == 10)
            CrosshairController.instance.Hide();
        base.StopSecondaryExecution();
    }

    public override void StopExecution()
    {
        if (module != null && module.gameObject.layer == 6 && layer == 10)
            CrosshairController.instance.Hide();
        base.StopExecution();
    }

    public override dynamic ReturnValue()
    {
        ExecuteCode();
        //print("val: " + val);
        //Debug.Log("Cursor direction: " + (Vector2)(object)val);
        return val;
    }
}
