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
    private bool executing = false;

    public override void ExecuteCode()
    {
        executing = true;
        
        rb = module.rb;
        col = module.col;
        tf = module.transform;
        anim = module.anim;

        base.ExecuteCode();
    }

    public override void StopExecution()
    {
        executing = false;
        rb.velocity = Vector2.zero;
        base.StopExecution();
    }

    private void Update()
    {
        if (!executing) return;
        bds = col.bounds;
        CheckWall();
        CheckGround();
        ClampRotations();
    }

    void CheckGround()
    {
        Vector2 origin = bds.center;
        Vector2 size = new Vector2(2f * bds.extents.x - 0.05f, 2 * bds.extents.y - 0.05f);
        Vector2 direction = -tf.up;
        
        RaycastHit2D hit = Physics2D.BoxCast(origin, size, 0f, direction, 0.1f, glm);;

        if (hit)
        {
            hasTurned = true;
            return;
        }
        if (!hasTurned) return;

        hasTurned = false;
        if (facingRight)
            tf.Rotate(tf.forward, -90f);
        else
            tf.Rotate(tf.forward, 90f);
        rb.velocity = Vector2.zero;
    }

    void CheckWall()
    {
        Vector2 origin = bds.center;
        Vector2 size = new Vector2(2f * bds.extents.x - 0.05f, 2 * bds.extents.y - 0.05f);
        Vector2 direction = tf.right;
        
        RaycastHit2D hit = Physics2D.BoxCast(origin, size, 0f, direction, 0.05f, glm);
        
        if (!hit) return;
        
        if (facingRight)
            tf.Rotate(tf.forward, 90f);
        else
            tf.Rotate(tf.forward, -90f);
        rb.velocity = Vector2.zero;
    }
    
    private void FixedUpdate()
    {
        if (!executing) return;
        if (!tf || !col || !rb) return;
        Move();
    }

    private bool facingRight = true;
    private void Move()
    {
        //Vector2 dir = (Vector2)(object)GetParameter(0);
        bool dir = (bool)(object)GetParameter(0);

        if ((facingRight && !dir) || (!facingRight && dir))
        {
            facingRight = !facingRight;
            tf.Rotate(tf.up, 180f);
        }

        float speed = module.moveSpeed * (float)(object)GetParameter(1);
        rb.velocity = tf.right * speed * module.moveSpeed;
    }

    private void ClampRotations()
    {
        Vector3 lea = tf.localEulerAngles;

        lea.x = Mathf.Round(lea.x / 90) * 90;
        lea.y = Mathf.Round(lea.y / 90) * 90;
        lea.z = Mathf.Round(lea.z / 90) * 90;

        tf.localEulerAngles = lea;
    }
}
