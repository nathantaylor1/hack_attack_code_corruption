using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Code : MonoBehaviour
{
    [SerializeField]
    protected string returnType = "void";
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
        {
            return null;
        }
        return nextBlockInput.GetCode();
    }

    public virtual void ExecuteCode()
    {
        Continue();
    }

    public virtual void Continue()
    {
        if (GetNext() != null)
            GetNext().ExecuteCode();
        else
            SignalCompletion();
    }

    public virtual dynamic ReturnValue()
    {
        ExecuteCode();
        return null;
    }

    public virtual void SignalCompletion()
    {
        /*if (transform.parent.TryGetComponent(out Code prevBlock))
        {
            prevBlock.SignalCompletion();
        }
        else if (transform.parent.TryGetComponent(out InputField parentField))
        {
            parentField.SignalCompletion();
        }*/
        Code prevBlock = transform.parent.GetComponentInParent<Code>();
        if (prevBlock != null)
        {
            prevBlock.SignalCompletion();
        }
    }
}
