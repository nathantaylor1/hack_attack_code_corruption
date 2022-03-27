using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBlock : CodeWithParameters
{
    public ContactFilter2D cf = new ContactFilter2D().NoFilter();
    // Check a maximum of 20 collisions
    private Collider2D[] cols = new Collider2D[20];
    public override void ExecuteCode()
    {
        float dmg = (float)(object)GetParameter(0);
        int k = module.col.OverlapCollider(cf, cols);
        int moduleId = module.gameObject.GetInstanceID();
        int daddyId = module.father.GetInstanceID();
        for (int i = 0; i < k; i++)
        {
            if (cols[i].gameObject.GetInstanceID() != moduleId && cols[i].gameObject.GetInstanceID() != daddyId ) {
                if (cols[i].TryGetComponent<HasHealth>(out HasHealth h)) {
                    h.Damage(dmg);
                }
                // Used to get the health of the printer
                if (cols[i].transform.parent != null && cols[i].transform.parent.TryGetComponent<HasHealth>(out HasHealth h1)) {
                    h1.Damage(dmg);
                }
            }
        }
        base.ExecuteCode();
    }
}