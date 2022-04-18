using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeSpace : MonoBehaviour
{
    public bool canExecute = true;
    [SerializeField]
    protected Code start;
    protected CodeModule module;
    protected bool shouldStop = false;
    //protected LinkedList<Code> codeTree;

    protected virtual void Awake()
    {
    }

    protected virtual void Update()
    {
        if (start != null && module != null)
        {
            start.SetModule(module);
            start.ExecuteSecondaryCode();
        }
    }

    public virtual void SetModule(CodeModule _module)
    {
        module = _module;
        /*if (start != null)
            start.SetModule(module);*/
    }

    public virtual void ToggleCanExecute(bool _canExecute)
    {
        if (!_canExecute && canExecute) {
            shouldStop = true;
        }
        canExecute = _canExecute;
    }

    protected virtual void StartExecution()
    {
        if (start != null && canExecute)
        {
            start.SetModule(module);
            start.ExecuteCode();
        }
        else if (start != null && !canExecute && shouldStop)
        {
            shouldStop = false;
            start.SetModule(module);
            start.StopExecution();
        }
    }

    protected virtual void EndExecution()
    {
        start.StopExecution();
    }
}
