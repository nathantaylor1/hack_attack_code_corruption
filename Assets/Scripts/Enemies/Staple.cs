using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staple : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            Destroy(gameObject);
        }
    }
}