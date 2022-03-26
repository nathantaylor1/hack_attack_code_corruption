using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBlock : MonoBehaviour
{
    public GameObject block;
    private bool alreadyPickedUp = false;

    public virtual void AddToInventory(GameObject inventory)
    {
        if (alreadyPickedUp) return;
        alreadyPickedUp = true;
        GameObject go = Instantiate(block, inventory.transform);
        SetValue(go);
        DestroyThis();
    }

    public virtual void SetValue(GameObject go)
    {
        
    }
    
    public virtual void DestroyThis()
    {
        Destroy(gameObject);
    }
}
