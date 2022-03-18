using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stapler : MonoBehaviour
{
    Rigidbody rb;
    public LayerMask layerMask;
    StapleShoot stapleShoot;
    Vector3 halfextents = new Vector3(.5f, .45f, .5f);
    public int elapsed = 0;
    bool shootMode = false;
    public float switchPositionRate = .15f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stapleShoot = GetComponentInChildren<StapleShoot>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("DSJJSDAJSADJDSA");
        //Debug.DrawRay(transform.position, (transform.right + transform.up * -1).normalized * 1f, Color.red);
        //Debug.Log(transform.forward);
        if(Physics.BoxCast(transform.position, halfextents, Vector3.down, out RaycastHit r, transform.rotation, .1f, ~layerMask))
        {
            elapsed += 1;
            if (elapsed < 20)
            {
                return;
            }
            //Debug.Log(r.transform.name);
            //Debug.Log("grounded");
            elapsed = 0;

            if (switchPositionRate > Random.Range(0f, 1f) && !shootMode) {
                shootMode = true;
                StartCoroutine(StartShooting());
            }

            if (!shootMode) {
                if (switchPositionRate > Random.Range(0f, 1f) ||
                    Physics.Raycast(transform.position, transform.right, 1f, ~layerMask) || 
                    !Physics.Raycast(transform.position, (transform.right + transform.up * -1).normalized, 1.2f, ~layerMask)
                    )
                {
                    transform.Rotate(Vector3.up, 180);
                }
                rb.AddForce((transform.right + transform.up * 5).normalized * 150);
            }
        }
    }
    private IEnumerator StartShooting() {
        // TODO open up stapler
        for (int i = 0; i < 3; i++)
        {
            stapleShoot.Shoot();
            yield return new WaitForSeconds(.5f);
        }
        shootMode = false;
    }

    private void OnDrawGizmos()
    {
        bool isHit = Physics.BoxCast(transform.position, halfextents, Vector3.down, out RaycastHit r, transform.rotation, .1f, ~layerMask);
        if (isHit)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + transform.up * -1 * r.distance, halfextents * 2);
        } else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position + transform.up * -1 * 2, halfextents * 2);
        }
    }

}
