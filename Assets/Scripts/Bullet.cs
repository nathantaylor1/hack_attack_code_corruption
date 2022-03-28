using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public string ID;
    private bool alreadyDamaged;
    public float damage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CheckID(other)) return; 
        if (!alreadyDamaged && other.gameObject.TryGetComponent<HasHealth>(out HasHealth h))
        {
            alreadyDamaged = true;
            h.Damage(damage);
        }
        Destroy(gameObject);
    }

    private bool CheckID(Collider2D other)
    {
        Stapler stapler = other.GetComponent<Stapler>();
        if (stapler == null) return false;
        if (stapler.ID == ID) return true;
        return false;
    }
}
