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

    // The amount of time that it takes before a checkpoint can be saved after a previous
    // save
    protected static float refreshTime = 0.1f;
    protected bool canSave = true;

    protected void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        EventManager.OnCheckpointSave.AddListener(UpdateCheckpoint);
        EventManager.OnPlayerDeath.AddListener(ResetToCheckpoint);

        sr.sprite = inactiveSprite;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.tag + " collided with checkpoint");
        if (collision.CompareTag("Player"))
        {
            SaveCheckpoint();
        }
    }

    protected IEnumerator SaveRefresh()
    {
        yield return new WaitForSeconds(refreshTime);
        canSave = true;
    }

    protected void SaveCheckpoint()
    {
        if (canSave)
        {
            isActive = true;
            sr.sprite = activeSprite;
            canSave = false;
            StartCoroutine(SaveRefresh());
            EventManager.OnCheckpointSave?.Invoke(GetInstanceID());
        }
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
