using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToPrinters : MonoBehaviour
{
    protected bool isStuck = false;
    protected Rigidbody2D printer = null;
    protected Rigidbody2D player = null;

    protected void Awake()
    {
        player = GetComponent<Rigidbody2D>();
    }

    protected void FixedUpdate()
    {
        if (isStuck && printer != null)
        {
            player.velocity = player.velocity + printer.velocity;
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Printer"))
        {
            //transform.parent = collision.gameObject.transform.parent;
            isStuck = true;
            printer = collision.rigidbody;
            Debug.Log("Stuck to printer");
        }
    }

    protected void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Printer"))
        {
            //transform.parent = null;
            isStuck = false;
            printer = null;
            Debug.Log("Un-stuck to printer");
        }
    }
}
