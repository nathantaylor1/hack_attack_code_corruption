using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBlock : MonoBehaviour
{
    public GameObject brokenMovableBox;
    public int breakVelocity = 12;
    
    private void OnCollisionStay2D(Collision2D other) {
        if (other.relativeVelocity.magnitude > 2) {
            CheckBreak(other);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        CheckBreak(other);
    }
    
    private void CheckBreak(Collision2D other) {
        float impulse=0f;
        foreach (ContactPoint2D cp in other.contacts) {
                impulse += cp.normalImpulse;
        }
        if(impulse > breakVelocity) {
            Break();
        }
    }
    
    private void Break() {
        gameObject.SetActive(false);
        
        GameObject box = Instantiate(brokenMovableBox, transform.position, transform.rotation);
        box.SetActive(true);
        
        Destroy(gameObject);
    }
}