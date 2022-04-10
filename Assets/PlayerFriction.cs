using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFriction : MonoBehaviour
{
    // Start is called before the first frame update
    public BoxCollider2D col;
    public PhysicsMaterial2D frictionless;
    public PhysicsMaterial2D friction;
    public LayerMask layermask;
    public int id = 0;

    private void OnTriggerEnter2D(Collider2D other) {
        if ( layermask == (layermask | (1 << other.attachedRigidbody.gameObject.layer))) {
        Debug.Log("hit " + other.name);
            col.sharedMaterial = frictionless;
            id = other.attachedRigidbody.gameObject.GetInstanceID();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (layermask == (layermask | (1 << other.attachedRigidbody.gameObject.layer))) {
            StartCoroutine(delay(other.attachedRigidbody.gameObject.GetInstanceID()));
        }
    }
    IEnumerator delay(int _id) {
        yield return new WaitForSeconds(.2f);
        if(id == _id) {
            col.sharedMaterial = friction;
            id = 0;
        }
    }
}
