using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : MonoBehaviour
{
    public float amplitude = 0.5f;
    public float frequency = 1f;
    //private SpriteRenderer spr;

    private void Awake()
    {
        //spr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        //if (!spr.isVisible) return;
        Vector3 tempPos = transform.position;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
    }
}
