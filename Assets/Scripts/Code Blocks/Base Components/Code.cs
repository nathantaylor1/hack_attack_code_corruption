using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Code : MonoBehaviour
{
    [SerializeField]
    protected string returnType = "void";
    protected InputField nextBlockInput;
    protected InputField bodyParent;

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
        return null;
    }

    public virtual void SetBodyParent(InputField _bodyParent)
    {
        bodyParent = _bodyParent;
    }

    public virtual InputField GetBodyParent()
    {
        return bodyParent;
    }

    public virtual void OnBodyCompletion()
    {
        //Continue();
    }

    public virtual void SignalCompletion()
    {
        /*Code prevBlock = transform.parent.GetComponentInParent<Code>();
        if (prevBlock != null)
        {
            prevBlock.SignalCompletion();
        }*/
        if (bodyParent != null)
            bodyParent.SignalCompletion();
    }
}
