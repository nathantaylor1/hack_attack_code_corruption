using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToPrinters : MonoBehaviour
{
    protected bool isStuck = false;
    protected Vector3 lastPos = Vector3.zero;
    protected Vector2 lastVel = Vector2.zero;
    protected Rigidbody2D printer = null;
    protected Rigidbody2D player = null;
    protected bool wasChanged = false;

    protected void Awake()
    {
        player = GetComponent<Rigidbody2D>();
        //lastPos = player.transform.position;
    }

    protected void FixedUpdate()
    {
        if (wasChanged && printer != null && player.velocity - printer.velocity != Vector2.zero)
        {
            Vector2 velChange = printer.velocity - lastVel;
            player.velocity -= velChange;
            wasChanged = false;
        }
        if (isStuck && printer != null)
        {
            //Debug.Log("lastPos: " + lastPos);
            //Vector3 change = printer.transform.position - lastPos;
            //player.transform.Translate(change, printer.transform);
            /*if (lastVel.x != 0 && printer.velocity.x == 0)
            {
                player.velocity = new Vector2(0, player.velocity.y);
            }*/
            //lastPos = printer.transform.position;
            Vector2 velChange = printer.velocity - lastVel;
            if (player.velocity - printer.velocity != Vector2.zero && !wasChanged)
            {
                player.velocity += velChange;
                wasChanged = true;
            }
            else
            {
                wasChanged = false;
            }
            lastVel = printer.velocity;
        }
        else
        {
            wasChanged = false;
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Printer"))
        {
            //transform.parent = collision.gameObject.transform.parent;
            isStuck = true;
            printer = collision.rigidbody;
            lastPos = printer.transform.position;
            lastVel = printer.velocity;
            //Debug.Log("Stuck to printer");
        }
    }

    protected void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Printer"))
        {
            //transform.parent = null;
            isStuck = false;
            //printer = null;
            //Debug.Log("Un-stuck to printer");
        }
    }
}
