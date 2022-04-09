using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCode : Code
{
    public override void ExecuteCode()
    {
        if (module.lastCollidedWith != null 
                && module.lastCollidedWith.TryGetComponent<CollectableBlock>(out CollectableBlock cb)) {
            cb.AddToInventory();
        }
        base.ExecuteCode();
    }
}
