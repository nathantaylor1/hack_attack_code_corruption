using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairMovement : MonoBehaviour
{
    Rigidbody rb;
    public LayerMask layerMask;
    StapleShoot stapleShoot;
    Vector3 halfextents = new Vector3(.5f, .45f, .5f);
    public int elapsed = 0;
    bool shootMode = false;
    public float switchPositionRate = .85f;
    public int power = 400;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude > 1)
        {
            return;
        }
        elapsed += 1;
        if (switchPositionRate > Random.Range(0f, 1f)) {
            return;
        }
        if (elapsed < 50)
        {
            return;
        }
        elapsed = 0;
        //Debug.Log(r.transform.name);
        //Debug.Log("grounded");
        int dir = 1;
        if (Random.Range(0, 2) == 0) {
            dir = -1;
        }
        rb.AddForce((transform.right * dir).normalized * power);
    }
}
