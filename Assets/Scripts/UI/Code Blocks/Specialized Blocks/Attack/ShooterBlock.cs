using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterBlock : AttackBlock
{
    private float projectileMoveSpeed = 5.0f;
    protected override void Awake()
    {
        // Assets/Resources/Prefabs/Bullet.prefab
        prefab = Resources.Load<GameObject>("Prefabs/Bullet");
        base.Awake();
    }

    public override void ExecuteCode()
    {
        if (prefab != null && canAttack)
        {
            dmg = (float)(object)GetParameter(0);
            Debug.Log("Shooting");
            var col = PlayerMovement.instance.playerCollider;
            var facingRight = PlayerMovement.instance.facingRight;

            GameObject go = Instantiate(prefab);
            Bullet bullet = go.GetComponent<Bullet>();
            bullet.damage = dmg;
            bullet.isPlayer = true;

            Vector3 pos = col.bounds.center;
            if (facingRight)
            {
                pos.x += (col.bounds.extents.x + bullet.c2d.bounds.extents.x + 0.05f);
            }
            else
            {
                pos.x -= (col.bounds.extents.x + bullet.c2d.bounds.extents.x + 0.05f);
            }

            go.transform.position = pos;
            var rb2d = go.GetComponent<Rigidbody2D>();
            var vel = rb2d.velocity;
            if (facingRight) vel = Vector2.right * projectileMoveSpeed;
            else vel = Vector2.left * projectileMoveSpeed;
            rb2d.velocity = vel;
            
            StartCoroutine(CO_Cooldown());
        }
        else
        {
            Debug.Log("Cannot Shoot");
        }
        base.ExecuteCode();
    }
}
