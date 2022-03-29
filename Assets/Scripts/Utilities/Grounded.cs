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
        Vector3 extents = bounds.extents * 2 + new Vector3(-0.1f, -0.2f, 0);
        //int layer = ~( (1 << col.gameObject.layer) | (1 << LayerMask.NameToLayer("Cursor")) );
        int layer = (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Enemies")) | (1 << LayerMask.NameToLayer("Movables"));
        
        if (Physics2D.BoxCast(bounds.center + Vector3.down * 0.1f, extents, 0f, Vector2.down, 0.05f, layer))
        {
            return true;
        }
        return false;
    }
}
