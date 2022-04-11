using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCode : Code
{
    public override void ExecuteCode()
    {
        /*if (module.lastCollidedWith != null 
                && module.lastCollidedWith.TryGetComponent<CollectableBlock>(out CollectableBlock cb)) {
            cb.AddToInventory();
        }*/
        var result = Physics2D.BoxCast(module.gameObject.transform.position + Vector3.up * 0.05f,
            module.col.bounds.size - Vector3.up * 0.1f, 0f, Vector2.down, 0.1f, 1 << LayerMask.NameToLayer("Collectables"));
        //Debug.DrawRay(module.gameObject.transform.position + Vector3.up * 0.05f, )
        if (result && result.collider.gameObject.TryGetComponent<CollectableBlock>(out CollectableBlock cb))
        {
            //Debug.Log("Hit block pickup");
            cb.AddToInventory();
        }
        base.ExecuteCode();
    }
}
