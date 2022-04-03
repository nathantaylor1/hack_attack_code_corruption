using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfPlatformCode : Code
{
    protected bool val = false;
    [Tooltip("Value between 0 and 1 inclusive to determine what percent of the body can be on the ledge")]
    public float groundedPercent = .7f;
    Vector3 offGroundPoint;

    public override void ExecuteCode()
    {
        Bounds bounds = module.col.bounds;
        Vector3 min = bounds.min;
        Vector3 max = bounds.max;
        float raycast_x = (max.x - min.x) * groundedPercent + min.x;
        offGroundPoint = new Vector3(raycast_x, min.y + .1f, 0);
        print("ray point " + offGroundPoint);

        // true if raycast does not hit anything
        val = !Physics2D.Raycast(offGroundPoint, Vector2.down, .15f, (1 << LayerMask.NameToLayer("Ground")));
        Debug.DrawRay(offGroundPoint, Vector3.down * .15f);
        base.ExecuteCode();
    }

    public override dynamic ReturnValue()
    {
        ExecuteCode();
        return val;
    }

}
