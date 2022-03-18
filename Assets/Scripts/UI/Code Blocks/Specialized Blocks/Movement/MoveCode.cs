using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCode : CodeWithParameters
{
    [Tooltip("Determines if script should add force to positive or negative x")] public bool moveRight;
    [Tooltip("Temp rigid body to add jump force while we dont have player class")] public Rigidbody2D rb2d;
    [Tooltip("Temp base move force while we dont have module class")] public float moveForce;
    
    public override void ExecuteCode()
    {
        float xVel = moveForce * (float)(object)GetParameter(0);
        if (!moveRight)
        {
            PlayerMovement.instance.facingRight = false;
            xVel *= -1;
        }
        else
        {
            PlayerMovement.instance.facingRight = true;
        }
        Vector3 currentVelocity = rb2d.velocity;
        currentVelocity.x = xVel;
        rb2d.velocity = currentVelocity;
        base.ExecuteCode();
    }

    public override void StopExecution() {
        var tempVel = rb2d.velocity;
        if (moveRight && tempVel.x > 0) {
            tempVel.x = 0;
        } else if (!moveRight && tempVel.x < 0) {
            tempVel.x = 0;
        }
        rb2d.velocity = tempVel;
        base.StopExecution();
    }
}
