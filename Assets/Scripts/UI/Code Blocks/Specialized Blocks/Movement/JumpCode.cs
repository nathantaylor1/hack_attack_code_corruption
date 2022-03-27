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
    private bool alreadyJumping;
    private float timeAllowed;

    public float coyoteTimeInSeconds = 0.15f;
    private float coyoteTime = 0f;

    public override void ExecuteCode()
    {
        if (!alreadyJumping && CanJump())
        {
            alreadyJumping = true;
            rb = module.rb;
            anim = module.anim;

            Vector2 currentVelocity = rb.velocity;
            currentVelocity.y = module.jumpSpeed * (float)(object)GetParameter(0);
            rb.velocity = currentVelocity;
            
            if (anim != null) 
                anim.SetTrigger("Jump");
            if (module.jumpSound != null && AudioManager.instance != null)
                AudioManager.instance.PlaySound(module.jumpSound, module.transform.position);
            StartCoroutine(ResetAlreadyJumping());
        }
        base.ExecuteCode();
    }

    /*
    // Coyote Time Calculations
    private void FixedUpdate()
    {
        if (!module.CompareTag("Player")) return;
        coyoteTime -= Time.fixedDeltaTime;
        if (Grounded.Check(module.col))
        {
            coyoteTime = coyoteTimeInSeconds;
        }
    } 
    */

    private bool CanJump()
    {
        if (module.CompareTag("Player"))
        {
            CoyoteTime ct = module.gameObject.GetComponent<CoyoteTime>();
            timeAllowed = ct.GetTimeAllowed();
            return ct.CanJump();;
        }
        return Grounded.Check(module.col);
    }

    private IEnumerator ResetAlreadyJumping()
    {
        yield return new WaitForSeconds(timeAllowed + 0.01f);
        alreadyJumping = false;
        yield return null;
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
