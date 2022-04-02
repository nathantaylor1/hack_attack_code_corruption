using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance;
    public static Dictionary<float, GameObject> collectedSoFar = new Dictionary<float, GameObject>();
    public static Dictionary<float, GameObject> savedAfterCheckpoint = new Dictionary<float, GameObject>();
    public static Dictionary<float, printID> sincePreviousCheckpoint = new Dictionary<float, printID>();
    public static Vector2 playerPos = Vector2.zero;
    public static GameObject inventory = null;
    public static float currentCheckpoint = 0;

    public static UnityEvent CheckpointUpdated = new UnityEvent();
    public static UnityEvent PlayerKilled = new UnityEvent();

    private void Awake() {
        instance = this;
        CheckpointUpdated.AddListener(UpdateCheckpoint);
        PlayerKilled.AddListener(OnPlayerKilled);
    }

    public void UpdateCheckpoint() {
        savedAfterCheckpoint = new Dictionary<float, GameObject>(collectedSoFar);
        sincePreviousCheckpoint = new Dictionary<float, printID>();
    }

    public void OnPlayerKilled() {

        foreach (var item in sincePreviousCheckpoint)
        {
            item.Value.gameObject.SetActive(true);
            item.Value.Rewind();
        }
        sincePreviousCheckpoint = new Dictionary<float, printID>();
    }

    // public void Reset() {
    //     foreach (var item in savedAfterCheckpoint.Values)
    //     {
    //         Destroy(item);
    //     }
    //     if (inventory != null) {
    //         Destroy(inventory);
    //     }

    //     collectedSoFar = new Dictionary<float, GameObject>();
    //     savedAfterCheckpoint = new Dictionary<float, GameObject>();
    //     playerPos = Vector2.zero;
    //     inventory = null;
    //     currentCheckpoint = 0;
    //     CheckpointUpdated = new UnityEvent();
    //     CheckpointManager.instance = null;
    // }
}
