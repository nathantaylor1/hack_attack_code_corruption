using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoyoteTime : MonoBehaviour
{
    private Collider2D c2d;
    [SerializeField] private float timeAllowed = 0.10f;
    private float time;
    
    private void Awake()
    {
        c2d = GetComponent<Collider2D>();
    }

    private void Update()
    {
        time -= Time.deltaTime;
        if (Grounded.Check(c2d))
            time = timeAllowed;
    }

    public bool CanJump()
    {
        return !(time < 0);
    }

    public float GetTimeAllowed()
    {
        return timeAllowed;
    }
}
