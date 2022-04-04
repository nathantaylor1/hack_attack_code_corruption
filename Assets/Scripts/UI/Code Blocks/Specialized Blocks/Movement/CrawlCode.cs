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
    private bool didCheck = false;
    private bool facingLeft = true;

    public override void ExecuteCode()
    {
        executing = true;
        
        rb = module.rb;
        col = module.col;
        tf = module.transform;
        anim = module.anim;
        if (executing && tf && col && rb) {
            Move();
        }
        didCheck = false;

        base.ExecuteCode();
    }

    public override void StopExecution()
    {
        executing = false;
        rb = module.rb;
        rb.velocity = Vector2.zero;
        base.StopExecution();
    }

    private void Update()
    {
        if (!executing || didCheck) return;
        bds = col.bounds;
        CheckWall();
        CheckGround();
        didCheck = true;
    }

    void CheckGround()
    {
        Vector2 origin = bds.center;
        Vector2 size = new Vector2(bds.extents.x / 2 - 0.05f, bds.extents.y / 2 - 0.05f);
        Vector2 direction = -tf.up;
        var offset = new Vector2();
        offset = -tf.right * (bds.extents.x / 2f);
        offset += (Vector2)(-tf.up * (bds.extents.x / 2f + .25f));

        RaycastHit2D hit = //Physics2D.Raycast(origin, direction, bds.extents.y + 0.05f, glm);
                           Physics2D.BoxCast(origin + offset, size, 0f, direction, 0.1f, glm);;
        if (!hasTurned && hit) return;
        if (hasTurned && !hit) return;
        // if (hit) hasTurned = false;
        if (hasTurned && hit)  {
            hasTurned = false;
            return;
        }

        if (facingLeft)
            tf.Rotate(tf.forward, -90f, Space.World);
        else
            tf.Rotate(tf.forward, 90f, Space.World);
        
        hasTurned = true;
        ClampRotations();
        rb.velocity = Vector2.zero;
        Debug.Log(tf.right);
        Debug.Log("called");
        // Debug.Break();
    }

    void CheckWall()
    {
        Vector2 origin = bds.center;
        Vector2 size = new Vector2(bds.extents.x -.05f, bds.extents.y -.05f);
        Vector2 direction = tf.right;
        
        var offset = new Vector2();
        offset = tf.right * (bds.extents.x / 2f + .25f);
        offset += (Vector2)(tf.up * bds.extents.x / 2f);
        RaycastHit2D hit = //Physics2D.Raycast(origin, direction, bds.extents.x + 0.05f, glm);
            Physics2D.BoxCast(origin + offset, size, 0f, direction, 0.05f, glm);
        
        if (!hit) return;
        
        Debug.Log(tf.right);
        if (facingLeft)
            tf.Rotate(tf.forward, 90f, Space.World);
        else
            tf.Rotate(tf.forward, -90f, Space.World);
        
        ClampRotations();
        rb.velocity = Vector2.zero;
        // Debug.Log(tf.right);
        // Debug.Log("called");
        // Debug.Break();
    }

    private void Move()
    {
        Vector2 par = (Vector2)(object)GetParameter(0);
        bool dir;
        if (tf.right.y == 0) {
            dir = Math.Sign(tf.right.x) != Math.Sign(par.x);
        } else {
            dir = Math.Sign(tf.right.y) != Math.Sign(par.y);
        }
        // care about x vice cersa
        // Debug.Log(tf.up);
        // Debug.Log("player dir " + par);
        // Debug.Log(tf.right);

        if (dir && !hasTurned)
        {
            facingLeft = !facingLeft;
            tf.Rotate(tf.up, 180, Space.World);
        }

        float speed = module.moveSpeed * (float)(object)GetParameter(1);
        if (speed < 0.05f && speed > -0.05f)
            anim.SetTrigger("Idle");
        else
            anim.SetTrigger("Run");
        rb.velocity = tf.right * speed;
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
