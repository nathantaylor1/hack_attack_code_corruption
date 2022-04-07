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
    public bool canFlip = true;
    public override void ExecuteCode()
    {
        executing = true;
        
        rb = module.rb;
        col = module.col;
        tf = module.transform;
        anim = module.anim;
        bds = col.bounds;
        var p0 = GetParameter(0);
        var p1 = GetParameter(1);
        if (executing && tf && col && rb && !(p0 is null) && !(p1 is null)) {
            if (CustomGrounded()) {
                rb.gravityScale = 0;
                Move((Vector2)(object) p0, (float)(object) p1);
                CheckWall();
                CheckGround();
            } else {
                rb.gravityScale = 1;
            }
        }

        base.ExecuteCode();
    }

    IEnumerator FlipBuffer() {
        yield return new WaitForSeconds(.5f);
        canFlip = true;
    }

    public override void StopExecution()
    {
        executing = false;
        rb = module.rb;
        // hackable basically means dead
        rb.gravityScale = 4;
        if (module.hackable) {
        }
        rb.velocity = Vector2.zero;
        base.StopExecution();
    }
    bool CustomGrounded() {
        Vector2 size3 = new Vector3(.3f, .3f, 0);
        int layer = (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Movables") | (1 << 29));
        var offset3 = (Vector2)tf.up * -1 * bds.extents.x;
        // how much wider should the check be than size
        var add = 1.1f;
        if (Mathf.Abs(tf.up.x) > Mathf.Abs(tf.up.y) ) {
            // means on a wall
            size3.y += add;
        } else {
            // means on floor or ceiling
            size3.x += add;
        }
        if (Physics2D.BoxCast(tf.position + (Vector3)offset3, size3, 0f, tf.up * -1, 0.05f, layer))
        {
            return true;
        } else {
            return false;
        }
    }

    void CheckGround()
    {
        Vector2 origin = bds.center;
        Vector2 size = new Vector2(bds.extents.x / 2 - 0.05f, bds.extents.y / 2 - 0.05f);
        Vector2 direction = -tf.up;
        var offset = new Vector2();
        offset = -tf.right * (bds.extents.x);
        offset += (Vector2)(-tf.up * (bds.extents.x / 2f + .35f));

        RaycastHit2D hit = Physics2D.BoxCast(origin + offset, size, 0f, direction, 0.1f, glm);
        if (!hasTurned && hit) return;
        if (hasTurned && !hit) return;
        if (hasTurned && hit)  {
            hasTurned = false;
            return;
        }

        tf.Rotate(tf.forward, -90f, Space.World);
        
        hasTurned = true;
        ClampRotations();
        rb.velocity = Vector2.zero;
        canFlip = false;
        StartCoroutine(FlipBuffer());
    }

    void CheckWall()
    {
        Vector2 origin = bds.center;
        Vector2 size = new Vector2(bds.extents.x -.05f, bds.extents.y -.05f);
        Vector2 direction = tf.right;
        
        var offset = new Vector2();
        offset = tf.right * (bds.extents.x / 2f + .05f);
        offset += (Vector2)(tf.up * bds.extents.x / 2f);
        RaycastHit2D hit = Physics2D.BoxCast(origin + offset, size, 0f, direction, 0.05f, glm);
        
        if (!hit) return;
        tf.Rotate(tf.forward, 90f, Space.World);
        
        ClampRotations();
        rb.velocity = Vector2.zero;
        canFlip = false;
        StartCoroutine(FlipBuffer());
    }

    private void Move(Vector2 p0, float p1)
    {
        Vector2 par = p0;
        bool dir;
        if (Mathf.RoundToInt(tf.right.y) == 0) {
            dir = Math.Sign(tf.right.x) != Math.Sign(par.x) && par.x != 0;
        } else {
            dir = Math.Sign(tf.right.y) != Math.Sign(par.y) && par.y != 0;
        }

        ClampRotations();
        if (dir && canFlip)
        {
            tf.Rotate(tf.up, 180, Space.World);
            canFlip = false;
            hasTurned = true;
            StartCoroutine(FlipBuffer());
        }

        float speed = module.moveSpeed * p1;
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