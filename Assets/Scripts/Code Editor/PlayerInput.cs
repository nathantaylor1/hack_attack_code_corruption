using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    public KeyCode keyCode;
    public Code start;
    // Start is called before the first frame update
    void Awake()
    {
    }

    public void Run() {
        if (Input.GetKeyDown(keyCode)) 
        {
            start.ExecuteCode();
        }
        if (Input.GetKeyUp(keyCode)) 
        {
            start.StopExecution();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (!EditorController.instance.is_in_editor) {
            Run();
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
