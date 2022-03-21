using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBlock : MonoBehaviour
{
    public GameObject block;
    public virtual void AddToInventory(GameObject inventory)
    {
        Instantiate(block, inventory.transform);
        Destroy(gameObject);
    }
}
