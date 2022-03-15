using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeSpace : MonoBehaviour
{
    [SerializeField]
    protected Code start;

    protected virtual void Awake()
    {
        
    }

    protected virtual void StartExecution()
    {
        start.ExecuteCode();
    }

    protected virtual void EndExecution()
    {
        start.StopExecution();
    }
}
