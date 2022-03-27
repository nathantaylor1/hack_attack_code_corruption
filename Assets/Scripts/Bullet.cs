using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool alreadyDamaged;
    public float damage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!alreadyDamaged && other.gameObject.TryGetComponent<HasHealth>(out HasHealth h))
        {
            alreadyDamaged = true;
            h.Damage(damage);
        }
        Destroy(gameObject);
    }
}
