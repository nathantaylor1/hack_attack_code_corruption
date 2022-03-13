using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // TODO move this to a player controller
    public Dictionary<FunctionOption, Code> codeToRun = new Dictionary<FunctionOption, Code>();
    public enum FunctionOption
    {
        MoveLeft,
        MoveRight,
        Jump
    };

    // All game state changes should happen here

    private void Awake()
    {
        // Not checking to see if another instance exists because if we switch scenes
        // then we'll want our EventManager instance to become the one for the current
        // scene
        instance = this;
    }

    private void Update() 
    {
        if (!EditorController.instance.editor_screen) {
            MoveLeft();
            MoveRight();
            Jump();
        }
    }

    public void MoveLeft() {
        if (Input.GetKeyDown(KeyCode.A)) 
        {
            if (codeToRun.TryGetValue(FunctionOption.MoveLeft, out Code code))
            {
                code.ExecuteCode();
            }
        }
    }

    public void MoveRight() 
    {
        if (Input.GetKeyDown(KeyCode.D)) 
        {
            if (codeToRun.TryGetValue(FunctionOption.MoveRight, out Code code))
            {
                code.ExecuteCode();
            }
        }
    }

    public void Jump() 
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            if (codeToRun.TryGetValue(FunctionOption.Jump, out Code code))
            {
               code.ExecuteCode();
            }
        }
    }
}
