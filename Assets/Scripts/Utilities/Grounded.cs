using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded
{
    /*public static bool Check(Collider2D col, int layerIndex)
    {
        Bounds bounds = col.bounds;
        Vector3 extents = bounds.extents * 2 + new Vector3(-0.1f, 0, 0);
        if (Physics2D.BoxCast(bounds.center, extents, 0f, Vector2.down, 0.05f, ~(1 << layerIndex)))
        {
            return true;
        }
        return false;
    }*/

    public static bool Check(Collider2D col)
    {
        Bounds bounds = col.bounds;
        Vector3 lowered_bounds = bounds.center +  new Vector3(0, -.2f,0);
        //lowered_bounds.y /= 2;
        Vector3 extents = bounds.extents + new Vector3(-0.1f, 0, 0);
        
        if (Physics2D.BoxCast(lowered_bounds, extents, 0f, Vector2.down, 0.05f, ~(1 << col.gameObject.layer)))
        {
            return true;
        }
        return false;
    }
}
