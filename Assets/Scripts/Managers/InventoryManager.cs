using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public GameObject player;
    // All game state changes should happen here

    private void Awake()
    {
        // There WILL be multiple instances
        // on checkpoint
        // get make a copy of this and add it to dont destroy on load
        // save it in checkpoint manager
        // this should only be null on load / reload
            instance = this;
            // CheckpointManager.CheckpointUpdated.AddListener(UpdateInventory);
            // if (CheckpointManager.inventory != null && CheckpointManager.inventory != gameObject) {
            //     foreach (Transform item in transform)
            //     {
            //         Destroy(item.gameObject);
            //     }
            //     foreach (Transform item in CheckpointManager.inventory.transform)
            //     {
            //         var b = Instantiate(item.gameObject, transform);
            //     }
            // }
    }

    public void UpdateInventory() {
        var temp = CheckpointManager.inventory;
        GameObject t = Instantiate(instance.gameObject);
        DontDestroyOnLoad(t);
        CheckpointManager.inventory = t;
        if (temp != null) {
            Destroy(temp);
        }
    }

    public void AddBlock(Code codeBlock) {
        codeBlock.transform.SetParent(transform);
    }

    private void OnDestroy() {
        if (instance == this) {
            instance = null;
        }
    }
}
