using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBlock : MonoBehaviour
{
    public int breakVelocity = 12;
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb)){
            float impulse=0f;
            foreach (ContactPoint2D cp in other.contacts) {
                    impulse += cp.normalImpulse;
            }
        // Debug.Log(impulse);
        if(impulse > breakVelocity) {
            Destroy(gameObject);
        }
        }
    }
}