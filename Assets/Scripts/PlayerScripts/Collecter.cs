using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collecter : MonoBehaviour
{
    private string collectableTag = "CollectableCodeBlock";
    public GameObject sidebarInventory;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(collectableTag))
        {
            col.GetComponent<CollectableBlock>().AddToInventory(sidebarInventory);
        }
    }
}
