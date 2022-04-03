using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    protected Sprite activeSprite;
    [SerializeField]
    protected Sprite inactiveSprite;
    protected SpriteRenderer sr;

    protected bool isActive = false;
    protected Collider2D col;

    protected void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        EventManager.OnCheckpointSave.AddListener(UpdateCheckpoint);
        EventManager.OnPlayerDeath.AddListener(ResetToCheckpoint);

        sr.sprite = inactiveSprite;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SaveCheckpoint();
        }
    }

    protected void SaveCheckpoint()
    {
        isActive = true;
        sr.sprite = activeSprite;
        EventManager.OnCheckpointSave?.Invoke(GetInstanceID());
    }

    protected void UpdateCheckpoint(int checkpointID)
    {
        if (checkpointID != GetInstanceID())
        {
            isActive = false;
            sr.sprite = inactiveSprite;
        }
    }

    protected void ResetToCheckpoint()
    {
        if (isActive)
        {
            
        }
    }
}
