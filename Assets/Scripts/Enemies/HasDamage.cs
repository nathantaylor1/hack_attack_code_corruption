using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasDamage : MonoBehaviour
{
    public int damage_amount = 1;
    public float velocityNeeded = 0;
    private Rigidbody2D rb;
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.TryGetComponent<HasHealth>(out HasHealth h)) {
                float impulse=0f;
                if (other.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb)){
                    ContactPoint2D[] s = new ContactPoint2D[other.contactCount];
                    int len = other.GetContacts(s);
                    for (int i = 0; i < len; i++)
                    {
                        ContactPoint2D cp = s[i];
                        impulse += cp.normalImpulse;
                        // Debug.Log("normal " + cp.normalImpulse);
                        // Debug.Log("tangent " + cp.tangentImpulse);
                        // Debug.Log("rv " + cp.relativeVelocity);
                    }
                }
                    // Debug.Log("player " + impulse);
                if( rb.velocity.magnitude > velocityNeeded && impulse > velocityNeeded) {
                    h.Damage(damage_amount);
                }
        }
    }
}