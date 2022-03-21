using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public Collider2D playerCollider;
    public bool facingRight;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }

        instance = this;
        playerCollider = gameObject.GetComponent<Collider2D>();
    }

    public void ChangeDirection(bool right)
    {
        if (right)
        {
            facingRight = true;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            facingRight = false;
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
