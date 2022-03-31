using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyDupes : printID
{
    protected override void Awake()
    {
        base.Awake();
        if (CheckpointManager.collectedSoFar.ContainsKey(id)) {
            Destroy(gameObject);
        }
    }
}