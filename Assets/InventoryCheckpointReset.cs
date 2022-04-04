using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCheckpointReset : CheckpointReset
{
    protected InventoryManager im;

    protected override void Awake()
    {
        im = GetComponent<InventoryManager>();
        base.Awake();
    }

    protected override void SaveToCheckpoint(int _)
    {
        base.SaveToCheckpoint(_);
        if (gameObject.activeInHierarchy)
        {
            InventoryManager.instance = im;
        }
    }

    protected override void ResetToCheckpoint()
    {
        if (!gameObject.activeInHierarchy)
        {
            InventoryManager.instance = im;
        }
        base.ResetToCheckpoint();
    }
}
