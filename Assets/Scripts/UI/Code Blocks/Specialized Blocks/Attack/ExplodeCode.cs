using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeCode : DamageBlock
{
    private readonly LayerMask hitLM = (1 << 6) | (1 << 10) | (1 << 29);
    [SerializeField]
    protected float radius = 4f;
    [SerializeField]
    protected ParticleSystem ps;

    public override void DoDamage()
    {
        int moduleId = module.gameObject.GetInstanceID();
        int daddyId = module.father.GetInstanceID();
        if (!col.isTrigger && col.gameObject.GetInstanceID() != moduleId && col.gameObject.GetInstanceID() != daddyId ) {
            int layer = col.gameObject.layer;
            if (!(layer == 6 || layer == 7 || layer == 10)) return;
            var p0 = GetParameter(0);
            if (!(p0 is null)) {
                module.rb.velocity = Vector3.zero;
                Explosion(moduleId, daddyId, (float)(object)p0);
            }
        }
    }

    private void Explosion(int self, int daddy, float dmg)
    {

        Collider2D[] cols = Physics2D.OverlapCircleAll(module.transform.position, radius);
        for (int i = 0; i < cols.Length; ++i)
        {
            if (cols[i].gameObject.GetInstanceID() != self 
                && cols[i].gameObject.GetInstanceID() != daddy) {
                HasHealth hh = cols[i].GetComponent<HasHealth>();
                if (hh) hh.Damage(dmg);
            }
        }
        if (ps != null)
            Instantiate(ps, transform.position, Quaternion.identity);
    }
}