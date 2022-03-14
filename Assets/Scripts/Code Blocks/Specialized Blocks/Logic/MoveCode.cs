using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCode : CodeWithParameters
{
    [Tooltip("Determines if script should add force to positive or negative x")] public bool is_right;
    [Tooltip("Temp rigid body to add jump force while we dont have player class")] public Rigidbody _rb;
    [Tooltip("Temp base move force while we dont have module class")] public float move_force;
    // Start is called before the first frame update
    public override void ExecuteCode()
    {
        float x_velocity = move_force * (float)(object)GetParameter(0);
        if (!is_right) x_velocity *= -1;
        Vector3 current_velocity = _rb.velocity;
        current_velocity.x = x_velocity;
        _rb.velocity = current_velocity;
        base.ExecuteCode();
    }

    public override void StopExecution() {
        var tempVel = _rb.velocity;
        if (is_right && tempVel.x > 0) {
            tempVel.x = 0;
        } else if (!is_right && tempVel.x < 0) {
            tempVel.x = 0;
        }
        _rb.velocity = tempVel;
        base.StopExecution();
    }
}
