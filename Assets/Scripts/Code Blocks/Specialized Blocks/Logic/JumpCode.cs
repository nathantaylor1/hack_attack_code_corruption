using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCode : CodeWithBodies
{
    [Tooltip("Temp rigid body to add jump force while we dont have player class")] public Rigidbody _rb;
    [Tooltip("Temp base jump force while we dont have module class")] public float jump_force;
    [Tooltip("Layer to raycast to make sure entity is touching the ground")]public LayerMask ground_layer;
    public override void ExecuteCode()
    {
        if (check_dir(Vector3.down)) {
            Vector2 current_velocity = _rb.velocity;
            current_velocity.y = jump_force * (float)(object)GetParameter(0);
            _rb.velocity = current_velocity;
        }
    }

    bool check_dir(Vector3 dir)
    {
        Collider col = GetComponentInChildren<Collider>();

        Ray ray = new Ray(col.bounds.center, dir);

        // Smaller than the actual radius of the collider, 
        //      so it doesn't catch on walls
        float radius = col.bounds.extents.x - .05f;

        // A bit below the bottom of the collider
        float full_distance = col.bounds.extents.y + .05f;

        if (Physics.SphereCast(ray, radius, full_distance, ground_layer))
            return true;
        else
            return false;
    }
}
