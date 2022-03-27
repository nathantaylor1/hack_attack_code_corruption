using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class JumpCode : CodeWithParameters
{
    //[Tooltip("Temp base jump force while we dont have module class")] public float jumpForce = 1.25f;
    //[Tooltip("Layer to raycast to make sure entity is touching the ground")]public LayerMask groundLayer;
    private Rigidbody2D rb;
    private Collider2D col;
    protected Animator anim;

    public override void ExecuteCode()
    {
        col = module.col;
        if (Grounded.Check(col)) {
            rb = module.rb;
            anim = module.anim;

            Vector2 currentVelocity = rb.velocity;
            currentVelocity.y = module.jumpSpeed * (float)(object)GetParameter(0);
            rb.velocity = currentVelocity;
            
            if (anim != null) 
                anim.SetTrigger("Jump");
            if (module.jumpSound != null && AudioManager.instance != null)
                AudioManager.instance.PlaySound(module.jumpSound, module.transform.position);
        }
        base.ExecuteCode();
    }

    // Used for debuging circlecast to make sure jump works correctly 
    // private void OnDrawGizmos()
    // {
    //     col = rb.gameObject.GetComponent<Collider2D>();
    //       Bounds bounds = col.bounds;
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
    //         Gizmos.DrawWireSphere(rb.gameObject.transform.position + Vector3.down * fullDist, col.bounds.extents.x -.05f);
    //         // Gizmos.DrawWireCube(transform.position + transform.up * -1 * isHit.distance, halfextents);
    //     }
    // }
}
