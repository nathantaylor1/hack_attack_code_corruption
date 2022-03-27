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
        yield return new WaitForSeconds(timeAllowed + 0.05f);
        alreadyJumping = false;
        yield return null;
    }
}
