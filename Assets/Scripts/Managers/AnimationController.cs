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

    protected void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    protected void Update()
    {
        /*if (!anim.GetCurrentAnimatorStateInfo(0).IsName(animationName + " Fall"))
        {
            
        }*/
        anim.SetBool("IsFalling", rb.velocity.y < 0 && !Grounded.Check(col));
    }
}
