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
    private readonly LayerMask groundAndMoveables = 1 << 7 | 1 << 16;
    private bool executing = false;
    public bool canFlip = true;
    public bool falling = false;
    bool isRunning = false;
    bool buffering = false;
    bool animationRunning = false;
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
            if (!falling && CustomGrounded()) {
                rb.gravityScale = 0;
                Move((Vector2)(object) p0, (float)(object) p1);
                CheckWall();
                CheckGround();
            } else if (module.lastCollidedWith != null 
            && !module.lastCollidedWith.isTrigger && rb.velocity.magnitude < .1f && falling) {
                // try flipping
                // Debug.Log(module.lastCollidedWith.isTrigger);
                falling = false;
                if (!CustomGrounded()) {
                    tf.Rotate(tf.forward, 180f, Space.World);
                }
                ClampRotations();
            }
            else {
                // falling
                // rb.gravityScale = module.gravityScale;
                module.EnableGravity();
                falling = true;
                isRunning = false;
                if (anim != null && anim.GetCurrentAnimatorStateInfo(0).IsName(module.animationName + " Run"))
                    anim.SetTrigger("Idle");
            }
        }

        base.ExecuteCode();
    }

    IEnumerator FlipBuffer() {
        if (!buffering) {
            buffering = true;
            yield return new WaitForSeconds(.5f);
            buffering = false;
            canFlip = true;
        }
    }

    public override void StopExecution()
    {
        executing = false;
        isRunning = false;
        if (module != null ) {
            rb = module.rb;
            // Debug.Log(module.transform.up);
            if (module.gameObject.layer == LayerMask.NameToLayer("Player") && module.transform.up != Vector3.up) {
                module.transform.up = Vector3.up;
            }
            rb.velocity = Vector2.zero;
            rb.gravityScale = module.gravityScale;
            anim = module.anim;
            if (anim != null && anim.GetCurrentAnimatorStateInfo(0).IsName(module.animationName + " Run"))
                anim.SetTrigger("Idle");
        }

        base.StopExecution();
    }
    bool CustomGrounded() {
        Vector2 size3 = new Vector3(.3f, .3f, 0);
        int layer = (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Movables") | (1 << 29));
        var offset3 = (Vector2)tf.up * -1 * bds.extents.y;
        // Debug.Log(bds.extents);
        // how much wider should the check be than size
        var add = 3f * bds.extents.x * 2;
        var addx = .2f;
        if (Mathf.Abs(tf.up.x) > Mathf.Abs(tf.up.y) ) {
            // means on a wall
            size3.y += add;
            size3.x += addx;
        } else {
            // means on floor or ceiling
            size3.x += add;
            size3.y += addx;
        }
        if (Physics2D.BoxCast(tf.position + (Vector3)offset3, size3, 0f, tf.up * -1, 0.05f, layer))
        {
            return true;
        } else {
            Debug.Log("fell");
            return false;
        }
    }

    void CheckGround()
    {
        var actualx = bds.extents.x;
        var actualy = bds.extents.y;
        var x = transform.up.x == 0 ? actualx : actualy;
        var y = transform.up.x == 0 ? actualy : actualx;
        Vector2 origin = tf.position;
        Vector2 size = new Vector2( .6f / 2f, (y * 2f / 3f));
        Vector2 direction = -tf.up;
        var offset = new Vector2();
        offset = -tf.right * (x);
        offset += (Vector2)(-tf.up * (x / 2f + .35f));
        LayerMask p = groundAndMoveables;
        p |= LayerMask.NameToLayer("OneWay");

        RaycastHit2D hit = Physics2D.BoxCast(origin + offset, size, 0f, direction, 0.1f, p);
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
        RaycastHit2D hit = Physics2D.BoxCast(origin + offset, size, 0f, direction, 0.05f, groundAndMoveables);
        
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
        isRunning = true;
        if (!animationRunning && anim != null) {
            animationRunning = true;
            StartCoroutine(AnimateRun());
        }

        rb.velocity = tf.right * p1 * module.crawlSpeed;
    }

    private void ClampRotations()
    {
        Vector3 lea = tf.localEulerAngles;

        lea.x = Mathf.Round(lea.x / 90) * 90;
        lea.y = Mathf.Round(lea.y / 90) * 90;
        lea.z = Mathf.Round(lea.z / 90) * 90;

        tf.localEulerAngles = lea;
    }

    protected IEnumerator AnimateRun()
    {
        while (isRunning)
        {
            if (Grounded.Check(col, tf) && !anim.GetCurrentAnimatorStateInfo(0).IsName(module.animationName + " Run") &&
                !anim.GetCurrentAnimatorStateInfo(0).IsName(module.animationName + " Jump"))
                anim.SetTrigger("Run");
            yield return null;
        }
        animationRunning = false;
    }
}