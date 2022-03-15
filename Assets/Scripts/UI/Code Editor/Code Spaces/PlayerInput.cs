using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : CodeSpace
{
    [SerializeField]
    private KeyCode keyCode;

    protected void Update()
    {
        if (!EditorController.instance.is_in_editor) {
            if (Input.GetKeyDown(keyCode))
            {
                StartExecution();
            }
            if (Input.GetKeyUp(keyCode))
            {
                EndExecution();
            }
        }
    }
    // public Code GetCode() {
    //     //get Start block
    //     foreach (Transform child in transform)
    //     {
    //         return child.GetComponent<StartCode>();
    //     }
    //     return null;
    // }
}
