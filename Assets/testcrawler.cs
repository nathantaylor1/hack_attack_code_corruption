
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class testcrawler : MonoBehaviour
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

    public float speed = 1;
    public Vector2 testDir = Vector2.left;
    public GameObject module;
    public bool canFlip = true;
    public bool bonked = false;

    private void FixedUpdate()
    {
        executing = true;
        

        rb = module.GetComponent<Rigidbody2D>();
        col = module.GetComponent<BoxCollider2D>();
        tf = module.transform;
        anim = module.GetComponent<Animator>();
        bds = col.bounds;
        if (executing && tf && col && rb) {
            if (!CustomGrounded()) {
                Debug.Log("NOT GROUNDED");
                rb.gravityScale = 1;
            } else {
                rb.gravityScale = 0;
                Move();
                CheckWall();
                CheckGround();
            }
        }
        didCheck = false;
    }
    IEnumerator testWait() {
        yield return new WaitForSeconds(.5f);
        canFlip = true;
    }

    // public override void StopExecution()
    // {
    //     executing = false;
    //     rb = module.rb;
    //     rb.velocity = Vector2.zero;
    //     base.StopExecution();
    // }
    void CheckGround()
    {
        Vector2 origin = tf.position;
        Vector2 size = new Vector2(bds.extents.x / 2 - 0.05f, bds.extents.y / 2 - 0.05f);
        Vector2 direction = -tf.up;
        var offset = new Vector2();
        offset = -tf.right * (bds.extents.x);
        offset += (Vector2)(-tf.up * (bds.extents.x / 2f + .35f));

        RaycastHit2D hit = Physics2D.BoxCast(origin + offset, size, 0f, direction, 0.1f, glm);;
        // if (hit == null)
        if (!hasTurned && hit) return;
        if (hasTurned && !hit) return;
        // if (hit) hasTurned = false;
        if (hasTurned && hit)  {
            hasTurned = false;
            return;
        }

        tf.Rotate(tf.forward, -90f, Space.World);
        
        hasTurned = true;
        ClampRotations();
        rb.velocity = Vector2.zero;
        canFlip = false;
        StartCoroutine(testWait());
        // Debug.Log(tf.right);
        // Debug.Log("called");
        // Debug.Break();
    }

    private void OnDrawGizmos() {
        // rb = module.GetComponent<Rigidbody2D>();
        // col = module.GetComponent<BoxCollider2D>();
        // tf = module.transform;
        // anim = module.GetComponent<Animator>();
        // // Debug.Log("ahhh");
        // Vector2 origin = bds.center;
        // var x = .5f;
        // var y = .5f;
        // Vector2 size = new Vector2(x -.05f, y -.05f);
        // Vector2 direction = tf.right;
        
        // var offset = new Vector2();
        // offset = tf.right * (x / 2f + .05f);
        // offset += (Vector2)(tf.up * x / 2f);
        // // RaycastHit2D hit = Physics2D.BoxCast(origin + offset, size, 0f, direction, 0.05f, glm);
        
        // Vector2 size2 = new Vector2(x / 2 - 0.05f, y / 2 - 0.05f);
        // Vector2 direction2 = -tf.up;
        // var offset2 = new Vector2();
        // offset2 = -tf.right * (x);
        // offset2 += (Vector2)(-tf.up * (x / 2f + .35f));

        // // RaycastHit2D hit2 = Physics2D.BoxCast(origin + offset2, size2, 0f, direction2, 0.1f, glm);;
        
        // Gizmos.color = Color.magenta;
        // var temp = new Vector3(size.x, size.y, 1);
        // var temp2 = new Vector3(size2.x, size2.y, 1);
        // // Debug.Log(bds.center);
        // Gizmos.DrawWireCube((Vector2)tf.position + offset, temp);
        // Gizmos.color = Color.yellow;
        // Gizmos.DrawWireCube((Vector2)tf.position + offset2, temp2);
        // Bounds bounds = col.bounds;
        // Vector2 size3 = new Vector3(.3f, .3f, 0);
        // //int layer = ~( (1 << col.gameObject.layer) | (1 << LayerMask.NameToLayer("Cursor")) );
        // int layer = (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Movables") | (1 << 29));
        // var offset3 = (Vector2)transform.up * -1 * .5f;
        // var add = 1.2f;
        // if (Mathf.Abs(transform.up.x) > Mathf.Abs(transform.up.y) ) {
        //     // means on a wall
        //     size3.y += add;
        // } else {
        //     // means on floor or ceiling
        //     size3.x += add;

        // }
        // if (Physics2D.BoxCast(transform.position + transform.up * -1 * 0.5f, size3, 0f, transform.up * -1, 0.05f, layer))
        // {
        //     Gizmos.color = Color.red;
        // } else {
        //     Gizmos.color = Color.yellow;
        // }

        // Gizmos.DrawWireCube((Vector2)transform.position + offset3, size3);
    }

    bool CustomGrounded() {
        tf = module.transform;
        Vector2 size3 = new Vector3(.3f, .3f, 0);
        int layer = (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Movables") | (1 << 29));
        var offset3 = (Vector2)tf.up * -1 * .5f;
        var add = 1.1f;
        if (Mathf.Abs(tf.up.x) > Mathf.Abs(tf.up.y) ) {
            // means on a wall
            size3.y += add;
        } else {
            // means on floor or ceiling
            size3.x += add;
        }
        if (Physics2D.BoxCast(tf.position + tf.up * -1 * 0.5f, size3, 0f, tf.up * -1, 0.05f, layer))
        {
            return true;
        } else {
            return false;
        }
    }

    void CheckWall()
    {
        Vector2 origin = bds.center;
        Vector2 size = new Vector2(bds.extents.x -.05f, bds.extents.y -.05f);
        Vector2 direction = tf.right;
        
        var offset = new Vector2();
        offset = tf.right * (bds.extents.x / 2f + .05f);
        offset += (Vector2)(tf.up * bds.extents.x / 2f);
        RaycastHit2D hit = //Physics2D.Raycast(origin, direction, bds.extents.x + 0.05f, glm);
            Physics2D.BoxCast(origin + offset, size, 0f, direction, 0.05f, glm);
        
        if (!hit) return;
        tf.Rotate(tf.forward, 90f, Space.World);
        
        ClampRotations();
        rb.velocity = Vector2.zero;
        canFlip = false;
        StartCoroutine(testWait());
        // Debug.Log(tf.right);
        // Debug.Log("called");
        // Debug.Break();
    }

    private void Move()
    {
        Vector2 par = testDir;
        bool dir;
        if (Mathf.RoundToInt(tf.right.y) == 0) {
            dir = Math.Sign(tf.right.x) != Math.Sign(par.x) && par.x != 0;
        } else {
            dir = Math.Sign(tf.right.y) != Math.Sign(par.y) && par.y != 0;
        }
        // care about x vice cersa
        // Debug.Log(tf.up);
        // // Debug.Log("player dir " + par);
        // Debug.Log(tf.right);
        // Debug.Log(dir);
        // Debug.Log((Mathf.RoundToInt(tf.right.y) == 0));
        // Debug.Log(Math.Sign(tf.right.x) != Math.Sign(par.x));
        // Debug.Log(par.x != 0);
        ClampRotations();
        if (dir && canFlip)
        {
            tf.Rotate(tf.up, 180, Space.World);
            // Debug.Break();
            canFlip = false;
            hasTurned = true;
            StartCoroutine(testWait());
        }

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