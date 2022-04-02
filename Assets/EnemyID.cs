using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyID : printID
{
    protected override void Awake()
    {
        base.Awake();
        CheckpointManager.CheckpointUpdated.AddListener(UpdateInfo);
    }

    public void UpdateInfo() {
        pos = transform.position;
    }

    public override void Rewind()
    {
        gameObject.SetActive(true);
        base.Rewind();
    }
}
