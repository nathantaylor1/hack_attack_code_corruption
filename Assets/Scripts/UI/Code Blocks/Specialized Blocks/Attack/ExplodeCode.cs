using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeCode : DamageBlock
{
    private readonly LayerMask hitLM = (1 << 6) | (1 << 10);
    public override void DoDamage()
    {
        int moduleId = module.gameObject.GetInstanceID();
        int daddyId = module.father.GetInstanceID();
        if (col.gameObject.GetInstanceID() != moduleId && col.gameObject.GetInstanceID() != daddyId ) {
            Explosion(moduleId);
        }
    }

    private void Explosion(int self)
    {

        // Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, radius, hitLM);
        // for (int i = 0; i < cols.Length; ++i)
        // {
        //     if (cols[i].gameObject.GetInstanceID() != self) continue;
        //     HasHealth hh = cols[i].GetComponent<HasHealth>();
        //     if (hh) hh.Damage(dmg);
        // }
    }
}