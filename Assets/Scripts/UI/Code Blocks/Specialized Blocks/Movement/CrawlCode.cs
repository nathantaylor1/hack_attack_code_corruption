using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CrawlCode : CodeWithParameters
{
    private Rigidbody2D rb;
    private Collider2D col;
    private Transform tf;
    private Animator anim;
    
    private float right;
    private bool facingRight = true;

    public override void ExecuteCode()
    {
        rb = module.rb;
        col = module.col;
        tf = module.transform;
        anim = module.anim;
        
        Rotate();
        GetDirection();

        if (!facingRight && right > 0)
        {
            Debug.Log("MOVE RIGIHT");
            facingRight = true;
            tf.Rotate(tf.up, 180f);
        }
        else if (facingRight && right < 0)
        {
            Debug.Log("MOVE LEFT ");
            facingRight = false;
            tf.Rotate(tf.up, 180f);
        }

        float speed = module.moveSpeed * (float)(object)GetParameter(1);
        Vector2 mvmt = tf.right * speed + (-tf.up * 2f);
        rb.velocity = mvmt;

        base.ExecuteCode();
    }

    private void GetDirection()
    {
        Vector2 dir = ((Vector2)(object)GetParameter(0)).normalized;
        float angle = tf.localEulerAngles.z;
        switch (Mathf.Abs(angle))
        {
            case 0: case 360:
                right = dir.x;
                break;
            
            case 180:
                right = -dir.x;
                break;
            
            case 90:
                right = dir.y;
                break;
            
            case 270:
                right = -dir.y;
                break;

            default:
                // Assert is in line with a surface (must be multiple of 90 degrees)
                float newAngle = Mathf.Round(angle / 90) * 90;
                tf.eulerAngles = new Vector3(tf.eulerAngles.x, tf.eulerAngles.y, newAngle);
                break;
        }
    }

    public override void StopExecution()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
        if (anim != null) anim.SetTrigger("Idle");
        base.StopExecution();
    }

    private bool canTurn, cooldown;
    private void Rotate()
    {
        if (cooldown) return;
        Vector3 origin = col.bounds.center;
        LayerMask lm = 1 << LayerMask.NameToLayer("Ground");
        if (!Physics2D.Raycast(origin, -tf.up, 0.5f, lm))
        {
            if (canTurn)
            {
                cooldown = true;
                Vector3 lea = tf.localEulerAngles;
                lea.z += 90f;
                tf.localEulerAngles = lea;
                canTurn = false;
            }
        }
        else
        {
            // Wait to be able to turn until after back on the ground
            canTurn = true;
        }
        if (Physics2D.Raycast(origin, tf.right, 1.0f, lm))
        {
            cooldown = true;
            Vector3 lea = tf.localEulerAngles;
            lea.z -= 90f;
            tf.localEulerAngles = lea;
        }

        if (cooldown) StartCoroutine(CO_Cooldown());
    }

    private IEnumerator CO_Cooldown()
    {
        yield return new WaitForSeconds(0.25f);
        cooldown = false;
        yield return null;
    }

}
