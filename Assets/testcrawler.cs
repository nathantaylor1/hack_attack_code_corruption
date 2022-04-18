
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
    //private bool didCheck = false;

    public float speed = 1;
    public Vector2 testDir = Vector2.left;
    public GameObject module;
    public bool canFlip = true;
    public bool falling = false;
    public bool buffering = false;
    public Collider2D lastCollidedWith = null;

    private void FixedUpdate()
    {
        executing = true;
        

        rb = module.GetComponent<Rigidbody2D>();
        col = module.GetComponent<BoxCollider2D>();
        tf = module.transform;
        anim = module.GetComponent<Animator>();
        bds = col.bounds;
        if (executing && tf && col && rb) {
            ExecuteCode();
            // if (!CustomGrounded()) {
            //     Debug.Log("NOT GROUNDED");
            //     rb.gravityScale = 1;
            // } else {
            //     rb.gravityScale = 0;
            //     Move();
            //     CheckWall();
            //     CheckGround();
            // }
        }
        //didCheck = false;
    }
    public void ExecuteCode()
    {
        if (!falling && CustomGrounded()) {
                rb.gravityScale = 0;
                Move(testDir, 1);
                CheckWall();
                CheckGround();
        } else if (lastCollidedWith != null 
        && !lastCollidedWith.isTrigger && rb.velocity.magnitude < .1f && falling) {
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
            rb.gravityScale = 2.5f;
            falling = true;
        }
    }
    IEnumerator FlipBuffer() {
        if (!buffering) {
            buffering = true;
            yield return new WaitForSeconds(.5f);
            buffering = false;
            canFlip = true;
        }
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

        RaycastHit2D hit = Physics2D.BoxCast(origin + offset, size, 0f, direction, 0.1f, glm);
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
        StartCoroutine(FlipBuffer());
        // Debug.Log(tf.right);
        // Debug.Log("called");
        // Debug.Break();
    }

    private void OnDrawGizmos() {
        rb = module.GetComponent<Rigidbody2D>();
        col = module.GetComponent<BoxCollider2D>();
        tf = module.transform;
        anim = module.GetComponent<Animator>();
        // Debug.Log("ahhh");
        Vector2 origin = bds.center;
        var actualx = .71f / 2f;
        var actualy = .93f / 2f;
        var x = transform.up.x == 0 ? actualx : actualy;
        var y = transform.up.x == 0 ? actualy : actualx;
        Vector2 size = new Vector2(x -.05f, y -.05f);
        Vector2 direction = tf.right;
        
        var offset = new Vector2();
        offset = tf.right * (x / 2f + .05f);
        offset += (Vector2)(tf.up * x / 2f);
        // RaycastHit2D hit = Physics2D.BoxCast(origin + offset, size, 0f, direction, 0.05f, glm);
        
        Vector2 size2 = new Vector2( .6f / 2f, (y * 2f / 3f));
        Vector2 direction2 = -tf.up;
        var offset2 = new Vector2();
        offset2 = -tf.right * (x);
        offset2 += (Vector2)(-tf.up * (x / 2f + .35f));

        // RaycastHit2D hit2 = Physics2D.BoxCast(origin + offset2, size2, 0f, direction2, 0.1f, glm);;
        
        Gizmos.color = Color.magenta;
        var temp = new Vector3(size.x, size.y, 1);
        var temp2 = new Vector3(size2.x, size2.y, 1);
        // Debug.Log(bds.center);
        Gizmos.DrawWireCube((Vector2)tf.position + offset, temp);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube((Vector2)tf.position + offset2, temp2);
        Bounds bounds = col.bounds;
        Vector2 size3 = new Vector3(.3f, .3f, 0);
        //int layer = ~( (1 << col.gameObject.layer) | (1 << LayerMask.NameToLayer("Cursor")) );
        int layer = (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Movables") | (1 << 29));
        var offset3 = (Vector2)transform.up * -1 * .5f;
        var add = 1.65f * x * 2;
        if (Mathf.Abs(transform.up.x) > Mathf.Abs(transform.up.y) ) {
            // means on a wall
            size3.y += add;
        } else {
            // means on floor or ceiling
            size3.x += add;

        }
        if (Physics2D.BoxCast(transform.position + transform.up * -1 * 0.5f, size3, 0f, transform.up * -1, 0.05f, layer))
        {
            Gizmos.color = Color.red;
        } else {
            Gizmos.color = Color.yellow;
        }

        Gizmos.DrawWireCube((Vector2)transform.position + offset3, size3);
    }

    bool CustomGrounded() {
        tf = module.transform;
        Vector2 size3 = new Vector3(.3f, .3f, 0);
        int layer = (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Movables") | (1 << 29));
        var offset3 = (Vector2)tf.up * -1 * .5f;
        var add = 1.45f * bds.extents.x * 2;
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
        StartCoroutine(FlipBuffer());
        // Debug.Log(tf.right);
        // Debug.Log("called");
        // Debug.Break();
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

        float speed = 2;
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

    private void OnCollisionEnter2D(Collision2D other) {
        lastCollidedWith = other.collider;
    }
    private void OnCollisionExit2D(Collision2D other) {
        lastCollidedWith = null;
    }
}