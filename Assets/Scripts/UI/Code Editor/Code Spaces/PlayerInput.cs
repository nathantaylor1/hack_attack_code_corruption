using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : CodeSpace
{
    [SerializeField]
    private KeyCode keyCode, altKeyCode;

    protected override void Update()
    {
        if (!EditorController.instance.is_in_editor) {
            if (Input.GetKey(keyCode) || altKeyCode != KeyCode.None && Input.GetKey(altKeyCode))
            {
                StartExecution();
            }
            if (Input.GetKeyUp(keyCode) || (altKeyCode != KeyCode.None && Input.GetKeyUp(altKeyCode)))
            {
                EndExecution();
            }
        }
        base.Update();
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
