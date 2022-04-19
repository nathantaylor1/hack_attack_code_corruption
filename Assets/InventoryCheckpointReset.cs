using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCheckpointReset : CheckpointReset
{
    protected InventoryManager im;
    //protected RectTransform rt;

    protected override void Awake()
    {
        im = GetComponent<InventoryManager>();
        //rt = GetComponent<RectTransform>();
        base.Awake();
        /*if (saveOnStart)
        {
            saveOnStart = false;
            SaveToCheckpoint(transform);
        }*/
    }

    //protected override void OnBecameVisible() { }

    public override void MarkForNoReset()
    {
        base.MarkForReset();
    }

    protected override void SaveToCheckpoint(Transform checkpoint)//int _)
    {
        MarkForReset();
        base.SaveToCheckpoint(checkpoint);
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
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
        //Debug.Log(gameObject.name + " is active in hierarchy: " + gameObject.activeInHierarchy);
    }
}
