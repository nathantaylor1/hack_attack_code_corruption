using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // All game state changes should happen here

    private void Awake()
    {
        // Not checking to see if another instance exists because if we switch scenes
        // then we'll want our EventManager instance to become the one for the current
        // scene
        instance = this;
    }
}
