using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeSpace : MonoBehaviour
{
    [SerializeField]
    protected Code start;
    protected CodeModule module;
    //protected LinkedList<Code> codeTree;

    protected virtual void Awake()
    {
    }

    public virtual void SetModule(CodeModule _module)
    {
        module = _module;
    }

    protected virtual void StartExecution()
    {
        if (start != null && module.isAlive != false)
        {
            start.SetModule(module);
            start.ExecuteCode();
        }
    }

    protected virtual void EndExecution()
    {
        if (module.isAlive != false) {
            start.StopExecution();
        }
    }
}
