using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysReset : CheckpointReset
{
    protected override void Awake()
    {
        base.MarkForReset();
        base.Awake();
    }

    public override void MarkForNoReset()
    {
        base.MarkForReset();
    }
}
