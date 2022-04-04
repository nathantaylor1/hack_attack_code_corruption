using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public float dmg = 2f;
    private Rigidbody2D rb;
    private SpriteRenderer spr;
    public float radius = 2.5f;
    private readonly LayerMask hitLM = (1 << 6) | (1 << 10);

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("BombEnemy")) return;
        
        int layer = col.gameObject.layer;
        if (!(layer == 6 || layer == 7 || layer == 10)) return;
        rb.velocity = Vector3.zero;
        Explosion();
        StartCoroutine(CO_Animate());
    }

    private void Explosion()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, radius, hitLM);
        for (int i = 0; i < cols.Length; ++i)
        {
            if (cols[i].CompareTag("BombEnemy")) continue;
            HasHealth hh = cols[i].GetComponent<HasHealth>();
            if (hh) hh.Damage(dmg);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private IEnumerator CO_Animate()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
        yield return null;
    }
}
