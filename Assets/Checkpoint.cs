using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : printID
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            if (id != CheckpointManager.currentCheckpoint) {
                // TODO should we allow saving at same checkpoint
                CheckpointManager.currentCheckpoint = id;
                CheckpointManager.playerPos = transform.position;
                CheckpointManager.CheckpointUpdated?.Invoke();
            }
        }
    }
}
