using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackCode : CodeWithParameters
{
    [SerializeField] private float attackRange = 1f;
    // players layer is: 7, enemies layer is: 10
    private int layer = (1 << 7) | (1 << 10);
    private bool canAttack = true;
    
    public override void ExecuteCode()
    {
        if (!canAttack) return;
        StartCoroutine(StartAttack());
        base.ExecuteCode();
    }

    IEnumerator StartAttack()
    {
        float damageMultiplier = (float)(object)GetParameter(1);
        float reloadSpeed = module.attackDelay / (float)(object)GetParameter(2);
        
        canAttack = false;
        
        Collider2D col = module.col;
        Bounds bounds = col.bounds;
        
        Vector2 direction = (Vector2)(object)GetParameter(0);

        // Sets center to be exactly next to collider based on direction
        Vector2 meleeCenter = new Vector3(bounds.center.x + (bounds.extents.x * direction.x), 
            bounds.center.y + (bounds.extents.y * direction.y));

        RaycastHit2D[] hits = Physics2D.BoxCastAll(meleeCenter, bounds.extents*2f, 0f, direction, attackRange, layer);

        foreach (RaycastHit2D hit in hits)
        {
            if (ReferenceEquals(module.transform.gameObject, hit.transform.gameObject)) continue;
            HasHealth hh = hit.transform.GetComponent<HasHealth>();
            if (hh) hh.Damage(module.meleeDamage * damageMultiplier);
        }

        /*Collider2D[] cols = Physics2D.OverlapBoxAll(meleeCenter, col.bounds.extents * 2, 0, layer);
        foreach (Collider2D collider in cols)
        {
            if(collider.TryGetComponent(out HasHealth has_health))
            {
                if (GameObject.ReferenceEquals(module.transform.gameObject, collider.gameObject))
                    continue;
                has_health.Damage(module.meleeDamage * damage);
            }
        }*/
        yield return new WaitForSeconds(reloadSpeed);
        canAttack = true;
    }
}
