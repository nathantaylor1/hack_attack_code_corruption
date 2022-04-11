using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBlock : CodeWithParameters
{
    protected Collider2D col = null;

    public override void ExecuteCode()
    {
        // need to update this in the case of the code block is dragged to a different module 
        col = module.lastCollidedWith;
        if (col != null) {
            DoDamage();
        }
        base.ExecuteCode();
    }

    public virtual void DoDamage() {
        var p0 = GetParameter(0);
        if (p0 is null) {
            return;
        }
        float dmg = (float)(object)p0;
        // Debug.Log("damaging" + dmg + "with k:" + k);
        int moduleId = module.gameObject.GetInstanceID();
        int daddyId = module.father.GetInstanceID();
            // Debug.Log(cols[i].name);
        if (!col.isTrigger && col.gameObject.GetInstanceID() != moduleId && col.gameObject.GetInstanceID() != daddyId ) {
            if (col.TryGetComponent<HasHealth>(out HasHealth h)) {
                h.Damage(dmg);
                // Debug.Log("damaging player");
            }
            // Used to get the health of the printer
            // if (col.transform.parent != null && col.transform.parent.TryGetComponent<HasHealth>(out HasHealth h1)) {
            //     h1.Damage(dmg);
            //     // Debug.Log("damaging printer");
            // }
        }
    }
}