using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StopPhantomCursor : MonoBehaviour
{
    public int order;
    public InputField input;
    public UnityEvent ended = new UnityEvent();
    // Start is called before the first frame update
    private void Start() {
        Debug.Log("started" + order);
        PhantomController.AddEnd(this);
        Listen();
    }

    protected virtual void Listen() {
        input.droppedInto.AddListener(ended.Invoke);
    }
}
