using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    // To declare a UnityEvent:
    // public static UnityEvent NewEvent1 = new UnityEvent();
    // public static UnityEvent<int> NewEvent2 = new UnityEvent<int>();

    // To invoke a UnityEvent (from another class):
    // EventManager.NewEvent1?.Invoke();
    // EventManager.NewEvent2?.Invoke(0);

    // To subscribe to a UnityEvent:
    // EventManager.NewEvent1.AddListener(NameOfFunctionWithoutParameters);
    // EventManager.NewEvent2.AddListener(NameOfFunctionWithIntParameter);

    // All events should be grouped under an appropriate heading, such as one of the 
    // following:

    // GAME STATE EVENTS

    // PLAYER EVENTS

    // EDITOR EVENTS
    public static UnityEvent<bool> OnToggleEditor = new UnityEvent<bool>();

    // etc.

    private void Awake()
    {
        // Not checking to see if another instance exists because if we switch scenes
        // then we'll want our EventManager instance to become the one for the current
        // scene
        instance = this;
    }
}
