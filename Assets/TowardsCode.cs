using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowardsCode : CodeWithParameters
{
    private Vector2 val = new Vector2(0, 0);
    public float detection_radius = 5f;
    [SerializeField] bool is_towards;
    // Start is called before the first frame update
    public override void ExecuteCode()
    {

        // Change to code space's object
        Transform trans = GameManager.instance.player.GetComponent<Transform>();
        int layer = (int)(object)GetParameter(0);
        //print("layer is " + layer);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(trans.position.x, trans.position.y), detection_radius, (1 << layer));
        Vector2 min_distance = new Vector2(1000f, 1000f);
        Collider2D closest_collider = null;

        // Find closest collider
        foreach (Collider2D col in colliders)
        {
            //print(col.gameObject);
            if (GameObject.ReferenceEquals(trans.gameObject, col.gameObject))
            {
                continue;
            }

            Vector2 temp_distance = trans.position - col.transform.position;
            if (temp_distance.magnitude < min_distance.magnitude)
            {
                closest_collider = col;
                min_distance = temp_distance;
            }
        }

        if (!closest_collider)
        {
            val = Vector2.zero;
            return;
        }

        if(closest_collider.GetComponent<Renderer>().isVisible && trans.GetComponent<Renderer>().isVisible)
        {
            if(is_towards)
            {
                val = closest_collider.transform.position - trans.position;
            }
            else
            {
                val = trans.position - closest_collider.transform.position;
            }
            val = val.normalized;
        }
        else
        {
            val = Vector2.zero;
        }

        base.ExecuteCode();
    }

    public override dynamic ReturnValue()
    {
        ExecuteCode();
        //print("val: " + val);
        return val;
    }
}
