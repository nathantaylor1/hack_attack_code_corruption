using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class JumpCode : CodeWithParameters
{
    [Tooltip("Temp rigid body to add jump force while we dont have player class")] public Rigidbody2D rb2d;
    [Tooltip("Temp base jump force while we dont have module class")] public float jumpForce;
    [Tooltip("Layer to raycast to make sure entity is touching the ground")]public LayerMask groundLayer;
    private Collider2D _collider2D;

    protected override void Awake() {
        base.Awake();
        _collider2D = rb2d.gameObject.GetComponent<Collider2D>();
    }
    public override void ExecuteCode()
    {
        if (check_dir(Vector3.down)) {
            Vector2 currentVelocity = rb2d.velocity;
            currentVelocity.y = jumpForce * (float)(object)GetParameter(0);
            rb2d.velocity = currentVelocity;
        }
        base.ExecuteCode();
    }

    bool check_dir(Vector3 dir)
    {
        Bounds bounds = _collider2D.bounds;
        Vector3 extents = bounds.extents;
        // Smaller than the actual radius of the collider, 
        //      so it doesn't catch on walls
        float radius = extents.x - .05f;
        // A bit below the bottom of the collider
        float fullDist = .2f;

        if (Physics2D.CircleCast(bounds.center, radius, dir, fullDist, groundLayer))
            return true;
        else
            return false;
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
