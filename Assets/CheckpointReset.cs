using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointReset : MonoBehaviour
{
    [HideInInspector]
    public bool saveOnStart = true;
    protected GameObject copy = null;
    protected bool shouldSave = false;
    protected int currentCheckpoint;
    private bool deleting = false;

    protected virtual void Awake()
    {
        EventManager.OnCheckpointSave.AddListener(SaveToCheckpoint);
        EventManager.OnPlayerDeath.AddListener(ResetToCheckpoint);
        if (saveOnStart)
        {
            saveOnStart = false;
            SaveToCheckpoint(0);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public virtual void MarkForReset()
    {

    }

    protected virtual void SaveToCheckpoint(int checkpoint)
    {
        if (deleting) {
            return;
        }
        if (shouldSave)
        {
            // State change if player reaches checkpoint without altering this gameobject
            if (gameObject.activeInHierarchy)
            {
                copy = Instantiate(gameObject, transform.position, transform.rotation, transform.parent);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        currentCheckpoint = checkpoint;
    }

    public void Deleting() {
        deleting = true;
    }

    protected virtual void ResetToCheckpoint()
    {
        if (deleting) {
            return;
        }
        if (shouldSave)
        {
            if (gameObject.activeInHierarchy)
            {
                Destroy(gameObject);
            }
            else
            {
                gameObject.SetActive(true);
                SaveToCheckpoint(0);
            }
        }
    }
}
