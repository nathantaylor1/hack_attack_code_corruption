using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlNoBlock : MonoBehaviour
{
    public float speed = 2.0f;
    private Rigidbody2D rb;
    private Collider2D col;
    private Bounds bds;
    private readonly LayerMask glm = 1 << 7 /*LayerMask.NameToLayer("Ground") is 7*/;
    private bool hasTurned;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        //Debug.Log("LayerMask: " + LayerMask.NameToLayer("Ground"));
    }

    private void Update()
    {
        CheckWall();
        CheckGround();
    }
    
    private void FixedUpdate()
    {
        bds = col.bounds;
        Move();
    }

    private void Move()
    {
        rb.velocity = transform.right * speed;
    }

    void CheckGround()
    {
        Vector2 origin = bds.center;
        Vector2 size = new Vector2(2f * bds.extents.x - 0.05f, 2 * bds.extents.y - 0.05f);
        Vector2 direction = -transform.up;
        
        RaycastHit2D hit = Physics2D.BoxCast(origin, size, 0f, direction, 0.05f, glm);;

        if (hit)
        {
            hasTurned = true;
            return;
        }

        if (!hasTurned) return;

        hasTurned = false;
        transform.Rotate(transform.forward, -90f);
    }

    void CheckWall()
    {
        Vector2 origin = bds.center;
        Vector2 size = new Vector2(2f * bds.extents.x - 0.05f, 2 * bds.extents.y - 0.05f);
        Vector2 direction = transform.right;
        
        RaycastHit2D hit = Physics2D.BoxCast(origin, size, 0f, direction, 0.05f, glm);;
        
        if (!hit) return;
        
        transform.Rotate(transform.forward, 90f);
    }
}
