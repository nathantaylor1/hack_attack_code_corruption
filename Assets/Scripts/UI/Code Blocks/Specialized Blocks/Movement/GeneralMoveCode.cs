using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

public class GeneralMoveCode : CodeWithParameters
{
    //[Tooltip("Determines if script should add force to positive or negative x")] public bool moveRight;
    //[Tooltip("Temp base move force while we dont have module class")] public float moveForce = 2.0f;
    private Rigidbody2D rb;
    protected Animator anim;
    protected Collider2D col;
    bool isRunning = false;
    bool animationRunning = false;

    public override void ExecuteCode()
    {
        rb = module.rb;
        anim = module.anim;
        col = module.col;
        var p0 = GetParameter(0);
        var p1 = GetParameter(1);

        if (!(p0 is null) && !(p1 is null)) {
            Vector2 dir = (Vector2)(object)p0;
            float speed = module.moveSpeed * (float)(object)p1;
            //Debug.Log("dir: " + dir);

            float xVel = Mathf.Abs(dir.normalized.x * speed) > Mathf.Abs(rb.velocity.x) || 
                (dir.x < 0 && rb.velocity.x > 0) || (dir.x > 0 && rb.velocity.x < 0) ? 
                dir.normalized.x * speed : rb.velocity.x;
            float yVel = Mathf.Abs(dir.normalized.y * speed) > Mathf.Abs(rb.velocity.y) ||
                (dir.y < 0 && rb.velocity.y > 0) || (dir.y > 0 && rb.velocity.y < 0) ?
                dir.normalized.y * speed : rb.velocity.y;
            rb.velocity = new Vector2(xVel, yVel);

            //module.transform.rotation = Quaternion.Euler(0, (dir.x < 0 ? 1 : 0) * 180, 0);
            if (Mathf.RoundToInt(Mathf.Sign(module.transform.right.x)) 
                 != Mathf.RoundToInt(Mathf.Sign(dir.x))) {
                module.transform.right *= -1;
            }
            // module.transform.localScale = new Vector3(dir.x < 0 ? -1 : 1, 1, 1);

            /*if (animationCoroutine != null)
                StopCoroutine(animationCoroutine);
            animationCoroutine = StartCoroutine(AnimateRun());*/

            if (!animationRunning && anim != null) {
                animationRunning = true;
                StartCoroutine(AnimateRun());
            }

            /*if (!isRunning && Grounded.Check(col))
            {
                anim.SetTrigger("Run");
            }*/

            isRunning = true;
        }
        base.ExecuteCode();
    }

    protected IEnumerator AnimateRun()
    {
        while (isRunning)
        {
            if (Grounded.Check(col) && 
                !anim.GetCurrentAnimatorStateInfo(0).IsName(module.animationName + " Run") &&
                !anim.GetCurrentAnimatorStateInfo(0).IsName(module.animationName + " Jump"))
                anim.SetTrigger("Run");
            yield return null;
        }
        animationRunning = false;
    }

    public override void StopExecution()
    {
        isRunning = false;
        if (module != null) {
            rb = module.rb;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (anim != null && anim.GetCurrentAnimatorStateInfo(0).IsName(module.animationName + " Run"))
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
