using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointReset : MonoBehaviour
{
    [HideInInspector]
    public bool saveOnStart = true;
    [HideInInspector]
    //public bool isCurrentCopy = false;
    //protected Vector3 position;
    protected GameObject copy = null;

    protected virtual void Awake()
    {
        EventManager.OnCheckpointSave.AddListener(SaveToCheckpoint);
        EventManager.OnPlayerDeath.AddListener(ResetToCheckpoint);
        //position = transform.position;
        if (saveOnStart)
        {
            //Debug.Log("saveOnStart is true");
            saveOnStart = false;
            //isCurrentCopy = true;
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
            /*if (copy != null)
            {
                Debug.Log("Destroying " + copy.name);
                Destroy(copy);
            }*/
            copy = Instantiate(gameObject, transform.position, transform.rotation, transform.parent);
            /*if (copy.TryGetComponent(out CheckpointReset cr))
            {
                cr.isCurrentCopy = false;
            }*/
            //Debug.Log("Saving " + gameObject.name + " to " + copy.name);
            //copy.SetActive(false);
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
            //Debug.Log("Restoring " + gameObject.name);
            /*if (copy != null)
            {
                Debug.Log(" from " + copy.name);
                copy.SetActive(true);
                if (copy.TryGetComponent(out CheckpointReset cr))
                {
                    cr.isCurrentCopy = true;
                }
            }*/
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
