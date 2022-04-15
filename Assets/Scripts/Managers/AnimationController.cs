using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    /*[SerializeField]
    [Tooltip("The name of this entity (used in its Animator)")]
    protected string animationName;*/
    protected Rigidbody2D rb;
    protected Collider2D col;
    protected Animator anim;
    protected CodeModule codeModule;

    protected void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        codeModule = GetComponent<CodeModule>();
    }

    protected void Update()
    {
        /*if (!anim.GetCurrentAnimatorStateInfo(0).IsName(animationName + " Fall"))
        {
            
        }*/
        bool isFalling = rb.velocity.y < 0 && !Grounded.Check(col, codeModule.transform);
        bool isGroundedJump = anim.GetCurrentAnimatorStateInfo(0).IsName(codeModule.animationName + " Jump") && Grounded.Check(col, codeModule.transform);
        anim.SetBool("IsFalling", isFalling || isGroundedJump);
    }
}
