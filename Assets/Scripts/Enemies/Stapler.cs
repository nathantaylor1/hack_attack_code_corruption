using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stapler : MonoBehaviour
{
    Rigidbody2D rb;
    public LayerMask layerMask;
    StapleShoot stapleShoot;
    Vector2 halfextents = new Vector3(1, 1);
    public int elapsed = 0;
    bool shootMode = false;
    public float switchPositionRate = .15f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stapleShoot = GetComponentInChildren<StapleShoot>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //Debug.Log("DSJJSDAJSADJDSA");
        //Debug.DrawRay(transform.position, (transform.right + transform.up * -1).normalized * 1f, Color.red);
        //Debug.Log(transform.forward);
        RaycastHit2D isHit = Physics2D.BoxCast(transform.position, halfextents, 0, Vector2.down, .05f, ~layerMask);
        if(isHit)
        {
            // Debug.Log("");
            elapsed += 1;
            if (elapsed < 20)
            {
                return;
            }
            //Debug.Log(r.transform.name);
            //Debug.Log("grounded");
            elapsed = 0;

            // if (switchPositionRate > Random.Range(0f, 1f) && !shootMode) {
                // Debug.Log(Physics2D.Raycast(transform.position, transform.right, gameObject.layer).transform.tag);
            if (Physics2D.Raycast(transform.position, transform.right, 3f, LayerMask.GetMask("Player")) &&
                !Physics2D.Raycast(transform.position, transform.right, 1f, LayerMask.GetMask("Player")) && !shootMode) {
                shootMode = true;
                StartCoroutine(StartShooting());
            }

            if (!shootMode) {
                if (
                    Physics2D.Raycast(transform.position, transform.right, 1f, ~layerMask) || 
                    !Physics2D.Raycast(transform.position, (transform.right + transform.up * -1).normalized, 1.2f, ~layerMask) ||
                    Physics2D.Raycast(transform.position, transform.right * -1, 3f, LayerMask.GetMask("Player"))
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
        RaycastHit2D isHit = Physics2D.BoxCast(transform.position, halfextents, 0, Vector2.down, .05f, ~layerMask);
        if (isHit)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + transform.up * -1 * isHit.distance, halfextents);
        } else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position + transform.up * -1 * 2, halfextents);
        }
    }

}
