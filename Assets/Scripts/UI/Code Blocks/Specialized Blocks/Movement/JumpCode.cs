using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class JumpCode : CodeWithParameters
{
    [Tooltip("Temp base jump force while we dont have module class")] public float jumpForce = 1.25f;
    [Tooltip("Layer to raycast to make sure entity is touching the ground")]public LayerMask groundLayer;
    private Rigidbody2D rb2d;
    private Collider2D _collider2D;
    protected Animator anim;

    protected override void Awake()
    {
        GameObject player = GameManager.instance.player;
        rb2d = player.GetComponent<Rigidbody2D>();
        _collider2D = player.GetComponent<Collider2D>();
        anim = player.GetComponent<Animator>();
        
        base.Awake();
    }
    public override void ExecuteCode()
    {
        if (Grounded.Check(_collider2D)) {
            Vector2 currentVelocity = rb2d.velocity;
            currentVelocity.y = jumpForce * (float)(object)GetParameter(0);
            rb2d.velocity = currentVelocity;
            anim.SetTrigger("Jump");
            print("pressed jump");
            AudioController.instance.PlayJump();
        }
        base.ExecuteCode();
    }

    // Used for debuging circlecast to make sure jump works correctly 
    // private void OnDrawGizmos()
    // {
    //     _collider2D = rb2d.gameObject.GetComponent<Collider2D>();
    //       Bounds bounds = _collider2D.bounds;
    //     Vector3 extents = bounds.extents;
    //     // Smaller than the actual radius of the collider, 
    //     //      so it doesn't catch on walls
    //     float radius = extents.x - .05f;
    //     // A bit below the bottom of the collider
    //     float fullDist = .2f;

    //     RaycastHit2D isHit = Physics2D.CircleCast(bounds.center, radius, Vector2.down, fullDist, groundLayer);
    //     if (isHit)
    //     {
    //         Gizmos.color = Color.red;
    //         Gizmos.DrawWireSphere(rb2d.gameObject.transform.position + Vector3.down * fullDist, _collider2D.bounds.extents.x -.05f);
    //         // Gizmos.DrawWireCube(transform.position + transform.up * -1 * isHit.distance, halfextents);
    //     }
    // }
}
