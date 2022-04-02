using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public int dir = 1;
    public float speed = .02f;
    Rigidbody2D rb;
    bool stop = false;
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!stop) {
            transform.position = transform.position + transform.right * dir * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer != LayerMask.NameToLayer("Ground")) {
            other.transform.parent = transform;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            StartCoroutine(wait());
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer != LayerMask.NameToLayer("Ground")) {
            other.transform.parent = transform.parent;
        }
    }
    IEnumerator wait() {
        stop = true;
        dir *= -1;
        yield return new WaitForSeconds(1f);
        stop = false;
    }
}
