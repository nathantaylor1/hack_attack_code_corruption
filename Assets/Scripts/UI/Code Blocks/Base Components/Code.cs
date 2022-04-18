using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Code : MonoBehaviour
{
    [SerializeField]
    protected string returnType = "void";
    protected CodeModule module;
    protected InputField nextBlockInput;

    public string ReturnType
    {
        get { return returnType; }
    }

    protected virtual void Awake()
    {
        nextBlockInput = GetComponent<InputField>();
    }

    protected virtual Code GetNext()
    {
        if (nextBlockInput == null)
            return null;
        return nextBlockInput.GetCode();
    }

    public virtual void SetModule(CodeModule _module)
    {
        module = _module;
    }

    public virtual void SetModuleRecursive(CodeModule _module)
    {
        module = _module;
        if (GetNext() != null)
        {
            GetNext().SetModuleRecursive(module);
        }
    }

    public virtual CodeModule GetModule()
    {
        return module;
    }

    public virtual void ExecuteCode()
    {
        Continue();
    }

    public virtual void ExecuteSecondaryCode()
    {
        if (GetNext() != null)
        {
            GetNext().SetModule(module);
            GetNext().ExecuteSecondaryCode();
        }
    }

    public virtual void StopExecution() {
        ContinueStop();
        StopSecondaryExecution();
    }

    public virtual void StopSecondaryExecution()
    {
        //Debug.Log("Stopping secondary execution on " + gameObject.name);
        if (GetNext() != null)
        {
            //Debug.Log("Next is not null");
            GetNext().SetModule(module);
            GetNext().StopSecondaryExecution();
        }
    }

    public virtual void Continue()
    {
        if (GetNext() != null) {
            GetNext().SetModule(module);
            GetNext().ExecuteCode();
        }
        else {
            // Debug.Log("done " + gameObject.name);
            SignalCompletion();
        }
    }

    public virtual void ContinueStop()
    {
        if (GetNext() != null) {
            // Debug.Log("executing " + gameObject.name);
            GetNext().SetModule(module);
            GetNext().StopExecution();
        }
        else {
            // Debug.Log("done " + gameObject.name);
            SignalCompletion();
        }
    }

    public virtual dynamic ReturnValue()
    {
        return null;
    }

    public virtual void SignalCompletion()
    {
        CodeWithBodies cwb = transform.parent.GetComponentInParent<CodeWithBodies>();
        if (cwb != null)
        {
            cwb.OnBodyCompletion();
        }
    }
}
