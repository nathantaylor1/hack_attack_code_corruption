using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CrawlCode : CodeWithParameters
{
    private Rigidbody2D rb;
    private Collider2D col;
    private Bounds bds;
    private Transform tf;
    private Animator anim;

    private bool hasTurned;
    private readonly LayerMask glm = 1 << 7;

    public override void ExecuteCode()
    {
        rb = module.rb;
        col = module.col;
        bds = col.bounds;
        tf = module.transform;
        anim = module.anim;

        CheckWall();
        CheckGround();

        Move();

        base.ExecuteCode();
    }

    public override void StopExecution()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
        base.StopExecution();
    }

    void CheckGround()
    {
        Vector2 origin = bds.center;
        Vector2 size = new Vector2(2f * bds.extents.x - 0.05f, 2 * bds.extents.y - 0.05f);
        Vector2 direction = -tf.up;
        
        RaycastHit2D hit = Physics2D.BoxCast(origin, size, 0f, direction, 0.05f, glm);;

        if (hit)
        {
            hasTurned = true;
            return;
        }

        if (!hasTurned) return;

        hasTurned = false;
        tf.Rotate(tf.forward, -90f);
    }

    void CheckWall()
    {
        Vector2 origin = bds.center;
        Vector2 size = new Vector2(2f * bds.extents.x - 0.05f, 2 * bds.extents.y - 0.05f);
        Vector2 direction = tf.right;
        
        RaycastHit2D hit = Physics2D.BoxCast(origin, size, 0f, direction, 0.05f, glm);;
        
        if (!hit) return;
        
        tf.Rotate(tf.forward, 90f);
    }
    
    private void Move()
    {
        //Vector2 dir = (Vector2)(object)GetParameter(0);
        bool dir = (bool)(object)GetParameter(0);
        float speed = module.moveSpeed * (float)(object)GetParameter(1);
        rb.velocity = transform.right * speed * (dir ? 1 : -1);
    }
}
