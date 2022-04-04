using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackCode : CodeWithParameters
{
    bool isAttaking = false;
    public override void ExecuteCode()
    {
        if (isAttaking) return;
        StartCoroutine(StartAttack());
        base.ExecuteCode();
    }

    IEnumerator StartAttack()
    {
        float damage = (float)(object)GetParameter(1);
        float reloadSpeed = module.attackDelay / (float)(object)GetParameter(2);
        print("reloadSpeed " + reloadSpeed);
        isAttaking = true;
        Collider2D col = module.col;
        Vector2 direction = (Vector2)(object)GetParameter(0);
        Vector3 entityCenter = col.bounds.center;

        // Sets center to be exactly next to collider based on direction
        Vector3 meleeCenter = new Vector3(entityCenter.x + col.bounds.extents.x * direction.x * 2, entityCenter.y + col.bounds.extents.y * direction.y * 2, 0);
        int layer = (1 << LayerMask.NameToLayer("Player")) | (1 << LayerMask.NameToLayer("Enemies"));

        Collider2D[] cols = Physics2D.OverlapBoxAll(meleeCenter, col.bounds.extents * 2, 0, layer);
        foreach (Collider2D collider in cols)
        {
            if(collider.TryGetComponent(out HasHealth has_health))
            {
                if (GameObject.ReferenceEquals(module.transform.gameObject, collider.gameObject))
                    continue;
                has_health.Damage(module.meleeDamage * damage);
            }
        }
        yield return new WaitForSeconds(reloadSpeed);
        isAttaking = false;

    }
}
