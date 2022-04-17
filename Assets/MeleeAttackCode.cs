using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackCode : CodeWithParameters
{
    [SerializeField] private float attackRange = 1.5f;
    // players layer is: 7, enemies layer is: 10
    private int layer = (1 << 9) | (1 << 10);
    private bool canAttack = true;

    public override void ExecuteCode()
    {
        if (!canAttack) return;

        Vector2 direction = (Vector2) (object) GetParameter(0);
        float dmg = module.meleeDamage * (float) (object) GetParameter(1);

        Bounds bounds = module.col.bounds;
        // Sets center to be exactly next to collider based on direction
        Vector3 meleeCenter = new Vector3(bounds.center.x + bounds.extents.x * direction.x * 1f, bounds.center.y + bounds.extents.y * direction.y * 2, 0);

        // Assets/Resources/DontMove/PMA.prefab
        GameObject pma = Resources.Load<GameObject>("DontMove/PMA");
        GameObject go = Instantiate(pma, meleeCenter, module.transform.rotation);
        
        RaycastHit2D[] hits = Physics2D.BoxCastAll(meleeCenter, bounds.extents*4f, 0f, direction, attackRange, layer);
        foreach (var hit in hits)
        {
            //Debug.Log($"hit: {hit.collider.name}");
            HasHealth hh = hit.collider.GetComponent<HasHealth>();
            if (hh) hh.Damage(dmg);
        }
        //Array.Clear(hits, 0, hits.Length);

        float cdTime = module.attackDelay / (float)(object)GetParameter(2);
        StartCoroutine(CO_Cooldown(cdTime, go));
        base.ExecuteCode();
    }

    private IEnumerator CO_Cooldown(float time, GameObject go)
    {
        canAttack = false;
        yield return new WaitForSeconds(time / 2);
        Destroy(go);
        yield return new WaitForSeconds(time / 2);
        canAttack = true;
    }
}
