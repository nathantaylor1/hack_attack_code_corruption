using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeSpace : MonoBehaviour
{
    protected bool canExecute = true;
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

    public virtual void ToggleCanExecute(bool _canExecute)
    {
        canExecute = _canExecute;
    }

    protected virtual void StartExecution()
    {
        if (start != null && canExecute)
        {
            start.SetModule(module);
            start.ExecuteCode();
        }
        else if (!canExecute)
        {
            start.StopExecution();
        }
    }

    protected virtual void EndExecution()
    {
        start.StopExecution();
    }
}
