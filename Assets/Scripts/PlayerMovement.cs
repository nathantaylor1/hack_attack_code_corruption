using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }

        instance = this;
        playerCollider = gameObject.GetComponent<Collider2D>();
    }

    public Collider2D playerCollider;
    public bool facingRight;
}
