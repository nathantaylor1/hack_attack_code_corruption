using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysExecuteCode : CodeSpace
{
    public int count = 10;
    private void FixedUpdate()
    {
        if (count < 0) {
            count = 10;
            StartExecution();
        }
        count -= 1;
    }
}
