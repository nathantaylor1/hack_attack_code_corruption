using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPhantomCursor : MonoBehaviour
{
    public int order;
    // Start is called before the first frame update
    private void Start() {
        PhantomController.instance.ends.Add(this);
    }
}
