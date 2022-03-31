using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class printID : MonoBehaviour
{
    // Start is called before the first frame update
    public float id;
    protected virtual void Awake()
    {
        // 0 means not set
        if (id == 0) {
            id = transform.position.x * 100000f + transform.position.y;
        }
    }

    public float GetID() {
        id = transform.position.x * 100000f + transform.position.y;
        return id;
    }

    public void Add() {
        CheckpointManager.collectedSoFar.Add(id, null);
    }
}
