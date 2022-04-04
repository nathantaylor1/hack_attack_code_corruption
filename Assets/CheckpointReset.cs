using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointReset : MonoBehaviour
{
    [HideInInspector]
    public bool saveOnStart = true;
    protected GameObject copy = null;

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

    protected virtual void SaveToCheckpoint(int _)
    {
        if (gameObject.activeInHierarchy)
        {
            copy = Instantiate(gameObject, transform.position, transform.rotation, transform.parent);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected virtual void ResetToCheckpoint()
    {
        if (gameObject.activeInHierarchy)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
