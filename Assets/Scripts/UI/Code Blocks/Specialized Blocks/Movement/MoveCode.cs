using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCode : CodeWithParameters
{
    [Tooltip("Determines if script should add force to positive or negative x")] public bool moveRight;
    [Tooltip("Temp base move force while we dont have module class")] public float moveForce = 1.0f;
    private Rigidbody2D rb2d;
    
    public override void ExecuteCode()
    {
        rb2d = GameManager.instance.player.GetComponent<Rigidbody2D>();
        float xVel = moveForce * (float)(object)GetParameter(0);
        if (!moveRight)
        {
            // player is/was moving left last
            PlayerMovement.instance.ChangeDirection(false);
            xVel *= -1;
        }
        else
        {
            // player is/was moving right last
            PlayerMovement.instance.ChangeDirection(true);
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
