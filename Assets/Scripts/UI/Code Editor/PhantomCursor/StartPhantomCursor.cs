using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPhantomCursor : MonoBehaviour
{
    // Update is called once per frame
    public int order;
    private void Start() {
        PhantomController.AddStart(this);
    }
}
