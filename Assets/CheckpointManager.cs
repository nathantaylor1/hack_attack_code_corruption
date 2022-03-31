using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance;
    public static Dictionary<float, GameObject> collectedSoFar = new Dictionary<float, GameObject>();
    public static Dictionary<float, GameObject> savedAfterCheckpoint = new Dictionary<float, GameObject>();
    public static Vector2 playerPos = Vector2.zero;
    public static GameObject inventory = null;
    public static float currentCheckpoint = 0;
    
    public static UnityEvent CheckpointUpdated = new UnityEvent();

    private void Awake() {
        instance = this;
        CheckpointUpdated.AddListener(UpdateCheckpoint);
    }
    public void UpdateCheckpoint() {
        savedAfterCheckpoint = collectedSoFar;
    }

    private void OnDestroy() {
        collectedSoFar = savedAfterCheckpoint;
    }
}
