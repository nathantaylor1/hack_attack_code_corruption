using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Collider2D c2d;
    public float damage;
    public bool isPlayer = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if ((!isPlayer && col.gameObject.CompareTag("Player"))
            || col.gameObject.CompareTag("Enemy"))
        {
            col.GetComponent<HasHealth>().Damage(damage);
        }
        Destroy(gameObject);
    }
}