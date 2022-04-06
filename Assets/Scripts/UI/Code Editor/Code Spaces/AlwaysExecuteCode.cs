using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysExecuteCode : CodeSpace
{
    public float count = .05f;
    public bool shouldWaitTimeBetween = true;
    private bool call = true;
    // public int startCount = 15;
    private void FixedUpdate()
    {
        if (call) {
            if (shouldWaitTimeBetween) {
                call = false;
                StartCoroutine(wait());
            }
            StartExecution();
        }
    }

    IEnumerator wait() {
        yield return new WaitForSeconds(count);
        call = true;
    }
}
