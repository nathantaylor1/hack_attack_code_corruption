using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// For Collectables
public class printID : MonoBehaviour
{
    // Start is called before the first frame update
    public float id;
    public Vector2 pos;
    public bool added = false;
    public UnityEvent rewind = new UnityEvent();
    protected virtual void Awake()
    {
        // 0 means not set
        if (id == 0) {
            GetID();
        }
        pos = transform.position;
    }

    public float GetID() {
        id = gameObject.GetInstanceID();
        return id;
    }

    public void Add() {
        if (!added) {
            added = true;
            CheckpointManager.sincePreviousCheckpoint.Add(id, this);
        }
    }
    public virtual void Rewind() {
        added = false;
        transform.position = pos;
        rewind.Invoke();
    }
}
