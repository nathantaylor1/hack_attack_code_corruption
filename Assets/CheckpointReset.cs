using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointReset : MonoBehaviour
{
    [HideInInspector]
    public bool saveOnStart = true;
    protected GameObject copy = null;
    protected Vector3 posCopy = Vector3.zero;
    protected Quaternion rotCopy = Quaternion.identity;
    protected CheckpointReset copyCR = null;
    protected bool hasSaved = false;
    protected bool shouldSave = false;
    protected bool shouldReset = false;
    //protected int currentCheckpoint;
    protected bool deleting = false;

    protected virtual void Awake()
    {
        EventManager.OnCheckpointSave.AddListener(SaveToCheckpoint);
        EventManager.OnPlayerDeath.AddListener(ResetToCheckpoint);
        posCopy = transform.position;
        rotCopy = transform.rotation;
        if (!saveOnStart)
        {
            gameObject.SetActive(false);
        }
    }

    protected virtual void OnBecameVisible()
    {
        if (saveOnStart && !hasSaved)
        {
            shouldSave = true;
            SaveToCheckpoint(transform);
        }
    }

    public virtual void MarkForReset()
    {
        Debug.Log("Marking " + gameObject.name + " for reset");
        shouldSave = true;
        shouldReset = true;
        if (copyCR != null)
        {
            copyCR.MarkForReset();
        }
    }

    public virtual void MarkForNoReset()
    {
        shouldSave = false;
        shouldReset = false;
        if (copyCR != null)
        {
            copyCR.MarkForNoReset();
        }
    }

    protected virtual void SaveToCheckpoint(Transform checkpoint)//int checkpoint)
    {
        if (deleting) {
            return;
        }
        if (shouldSave)
        {
            saveOnStart = false;
            hasSaved = true;
            if (gameObject.activeInHierarchy)
            {
                copy = Instantiate(gameObject, transform.position, transform.rotation, transform.parent);
                copyCR = copy.GetComponent<CheckpointReset>();
            }
            else
            {
                Destroy(gameObject);
            }
            MarkForNoReset();
        }
        posCopy = transform.position;
        rotCopy = transform.rotation;
        //currentCheckpoint = checkpoint;
    }

    public void Deleting() {
        deleting = true;
    }

    protected virtual void ResetToCheckpoint()
    {
        if (deleting) {
            return;
        }
        if (shouldReset/* && hasSaved*/)
        {
            if (gameObject.activeInHierarchy)
            {
                Debug.Log("Destroying " + gameObject.name);
                Destroy(gameObject);
            }
            else
            {
                gameObject.SetActive(true);
                shouldSave = true;
                SaveToCheckpoint(transform);
            }
        }
        else
        {
            transform.position = posCopy;
            transform.rotation = rotCopy;
        }
    }
}
