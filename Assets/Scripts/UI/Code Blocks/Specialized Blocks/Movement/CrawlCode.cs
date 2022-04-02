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

    public override void ExecuteCode()
    {
        rb = module.rb;
        col = module.col;
        tf = module.transform;
        anim = module.anim;
        
        GetDirection();

        Vector2 mvmt = tf.right * right;
        rb.velocity = mvmt;
        if (mvmt == Vector2.zero)
            if (anim != null) anim.SetTrigger("Idle");
        else
            if (anim != null) anim.SetTrigger("Run");

        base.ExecuteCode();
    }

    private void GetDirection()
    {
        Vector2 dir = ((Vector2)(object)GetParameter(0)).normalized;
        float speed = module.moveSpeed * (float)(object)GetParameter(1);
        float angle = tf.localEulerAngles.z;
        switch (Mathf.Abs(angle))
        {
            case 0: case 360:
                right = dir.x * speed;
                break;
            
            case 180:
                right = -dir.x * speed;
                break;
            
            case 90:
                right = dir.y * speed;
                break;
            
            case 270:
                right = -dir.y * speed;
                break;

            default:
                // Assert is in line with a surface (must be multiple of 90 degrees)
                float newAngle = Mathf.Round(angle / 90) * 90;
                tf.eulerAngles = new Vector3(0f, 0f, newAngle);
                break;
        }
    }

    public override void StopExecution()
    {
        rb.velocity = Vector2.zero;
        if (anim != null) anim.SetTrigger("Idle");
        base.StopExecution();
    }
}
