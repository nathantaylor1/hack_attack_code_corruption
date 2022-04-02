using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StopPhantomClick : StopPhantomCursor
{
    public EditorButton buttonToClick;
    protected override void Listen() {
        buttonToClick.clicked.AddListener(ended.Invoke);
    }
}