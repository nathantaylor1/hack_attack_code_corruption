using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (CheckpointManager.playerPos != Vector2.zero) {
            transform.position = CheckpointManager.playerPos;
        }
    }
}
