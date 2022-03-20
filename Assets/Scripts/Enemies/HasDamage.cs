using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasDamage : MonoBehaviour
{
    public int damage_amount = 1;
    public int velocityNeeded = 0;
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.TryGetComponent<HasHealth>(out HasHealth h)
            && other.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > velocityNeeded) {
                h.Damage(damage_amount);
                Debug.Log(gameObject.name);
        }
    }
}