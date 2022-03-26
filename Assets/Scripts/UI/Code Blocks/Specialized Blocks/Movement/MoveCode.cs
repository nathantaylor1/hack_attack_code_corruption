using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

public class MoveCode : CodeWithParameters
{
    [Tooltip("Determines if script should add force to positive or negative x")] public bool moveRight;
    [Tooltip("Temp base move force while we dont have module class")] public float moveForce = 1.0f;
    private Rigidbody2D rb2d;
    protected Animator anim;
    protected Collider2D col;
    bool isRunning = false;
    Coroutine animationCoroutine;
    
    public override void ExecuteCode()
    {
        rb2d = module.rb;
        anim = module.anim;
        col = module.col;
        isRunning = true;

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

        if (animationCoroutine != null)
            StopCoroutine(animationCoroutine);
        animationCoroutine = StartCoroutine(AnimateRun());

        base.ExecuteCode();
    }

    protected IEnumerator AnimateRun()
    {
        while (isRunning)
        {
            if (Grounded.Check(col) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Player Run") &&
                !anim.GetCurrentAnimatorStateInfo(0).IsName("Player Jump"))
                anim.SetTrigger("Run");
            yield return null;
        }
    }

    public override void StopExecution() {
        isRunning = false;

        var tempVel = rb2d.velocity;
        if (moveRight && tempVel.x > 0) {
            tempVel.x = 0;
        } else if (!moveRight && tempVel.x < 0) {
            tempVel.x = 0;
        }
        rb2d.velocity = tempVel;

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Player Run"))
            anim.SetTrigger("Idle");

        base.StopExecution();
    }/*

    bool IsGrounded()
    {
        Bounds bounds = col.bounds;
        Vector3 extents = bounds.extents * 2 + new Vector3(-0.1f, 0, 0);
        // Layer mask will have to change once adapted for non-player entities
        if (Physics2D.BoxCast(bounds.center, extents, 0f, Vector2.down, 0.05f, ~(1 << 6)))
        {
            return true;
        }
        return false;
    }*/
}
