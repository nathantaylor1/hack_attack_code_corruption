using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class printID : MonoBehaviour
{
    // Start is called before the first frame update
    public float id;
    public Vector2 pos;
    protected virtual void Awake()
    {
        // 0 means not set
        if (id == 0) {
            id = transform.position.x * 100000f + transform.position.y;
        }
        pos = transform.position;
    }

    public float GetID() {
        id = transform.position.x * 100000f + transform.position.y;
        return id;
    }

    public void Add() {
        CheckpointManager.sincePreviousCheckpoint.Add(id, this);
    }
    public void Rewind() {
        
    }
}
