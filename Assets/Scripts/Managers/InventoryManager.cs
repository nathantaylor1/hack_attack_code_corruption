using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    //public GameObject player;
    // All game state changes should happen here

    private void Awake()
    {
        // Not checking to see if another instance exists because if we switch scenes
        // then we'll want our EventManager instance to become the one for the current
        // scene

        /*if (instance != null)
        {
            //Debug.Log("Already a GameManager");
            Destroy(instance);
        }*/

        //instance = this;
    }

    public void AddBlock(Code codeBlock) {
        codeBlock.transform.SetParent(transform);
        if (codeBlock.gameObject.TryGetComponent(out Draggable dr))
        {
            dr.ToggleGlow(true);
        }
    }
}
