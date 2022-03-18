using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public int elapsed = 0;
    public float switchPositionRate = .95f;
    public int power = 400;
    public LayerMask walls;
    int dir = 1;
    bool sawPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude > 1)
        {
            return;
        }
        if (Physics2D.Raycast(transform.position, Vector2.right * dir, 3f, LayerMask.GetMask("Player"))) {
            sawPlayer = true;
        } else if (Physics2D.Raycast(transform.position, Vector2.right * dir * -1, 3f, LayerMask.GetMask("Player"))) {
            dir *= -1;
            sawPlayer = true;
        }
        if (switchPositionRate > Random.Range(0f, 1f)) {
            return;
        }
        elapsed += 1;
        if (elapsed < 50)
        {
            return;
        }
        elapsed = 0;
        if (!sawPlayer && Random.Range(0, 4) == 0) {
            dir *= -1;
        }
        if (Physics2D.Raycast(transform.position, Vector2.right * dir, 1, ~walls)) {
            dir *= -1;
        }
        sawPlayer = false;
        //Debug.Log(r.transform.name);
        //Debug.Log("grounded");
        rb.AddForce((transform.right * dir).normalized * power);
    }
}
