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
        if (saveOnStart)
        {
            saveOnStart = false;
            SaveToCheckpoint(transform);
        }
    }

    protected override void OnBecameVisible() { }

    public override void MarkForNoReset()
    {
        base.MarkForReset();
    }

    protected override void SaveToCheckpoint(Transform checkpoint)//int _)
    {
        MarkForReset();
        base.SaveToCheckpoint(checkpoint);
        if (gameObject.activeInHierarchy)
        {
            InventoryManager.instance = im;
        }
    }

    protected override void ResetToCheckpoint()
    {
        MarkForReset();
        if (!gameObject.activeInHierarchy)
        {
            InventoryManager.instance = im;
        }
        base.ResetToCheckpoint();
        Debug.Log(gameObject.name + " is active in hierarchy: " + gameObject.activeInHierarchy);
    }
}
