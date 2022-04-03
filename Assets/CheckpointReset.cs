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
        if (saveOnStart)
        {
            //Debug.Log("saveOnStart is true");
            saveOnStart = false;
            SaveToCheckpoint(0);
        }
        EventManager.OnCheckpointSave.AddListener(SaveToCheckpoint);
        EventManager.OnPlayerDeath.AddListener(ResetToCheckpoint);
    }

    protected virtual void SaveToCheckpoint(int _)
    {
        if (copy != null)
        {
            Destroy(copy);
        }
        //Debug.Log("Before executing Instantiate");
        copy = Instantiate(gameObject, transform.position, Quaternion.identity);
        //Debug.Log("After executing Instantiate");
        //Debug.Log("Original saveOnStart: " + saveOnStart);
        //Debug.Log("Copy saveOnStart: " + copy.GetComponent<CheckpointReset>().saveOnStart);
        copy.SetActive(false);
    }

    protected virtual void ResetToCheckpoint()
    {
        if (copy != null)
        {
            copy.SetActive(true);
        }
        Destroy(gameObject);
    }
}
