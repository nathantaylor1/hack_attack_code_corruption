using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableFloat : CollectableBlock
{
    public float value;
    public override void AddToInventory(GameObject inventory)
    {
        GameObject go = Instantiate(block, inventory.transform);
        FloatCode fc = go.GetComponent<FloatCode>();
        fc.val = value;
        fc.SetText();
        Destroy(gameObject);
    }
}
