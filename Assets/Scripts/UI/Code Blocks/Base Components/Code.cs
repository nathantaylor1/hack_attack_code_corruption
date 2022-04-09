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

    public virtual void ExecuteCode()
    {
        Continue();
    }

    public virtual void StopExecution() {
        ContinueStop();
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
