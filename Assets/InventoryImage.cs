using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryImage : MonoBehaviour
{
    public static InventoryImage instance;

    private void Awake()
    {
        instance = this;
    }
}
