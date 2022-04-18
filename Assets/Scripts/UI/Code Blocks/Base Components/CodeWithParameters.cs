using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeWithParameters : Code
{
    protected List<InputField> parameters = new List<InputField>();

    protected override void Awake()
    {
        base.Awake();
        Transform signature = transform.Find("Background/Signature");//.Find("Signature");
        // Should only iterate through top layer
        foreach (Transform parameterTrans in signature)
        {
            if (parameterTrans.TryGetComponent(out InputField parameter))
            {
                if (parameter.GetInstanceID() != GetInstanceID())
                    parameters.Add(parameter);
                else
                    nextBlockInput = parameter;
            }
        }
        /*foreach (InputField parameter in transform.GetComponentsInChildren<InputField>())
        {
            if (parameter.GetInstanceID() != GetInstanceID())
                parameters.Add(parameter);
            else
                nextBlockInput = parameter;
        }*/
    }

    public override void ExecuteSecondaryCode()
    {
        if (module != null)
        {
            foreach (InputField c in parameters)
            {
                if (c.GetCode() != null)
                {
                    c.GetCode().SetModule(module);
                    c.GetCode().ExecuteSecondaryCode();
                }
            }
        }
        base.ExecuteSecondaryCode();
    }

    public override void StopSecondaryExecution()
    {
        if (module != null)
        {
            foreach (InputField c in parameters)
            {
                if (c.GetCode() != null)
                {
                    c.GetCode().SetModule(module);
                    c.GetCode().StopSecondaryExecution();
                }
            }
        }
        base.StopSecondaryExecution();
    }

    protected virtual dynamic GetParameter(int index)
    {
        Code c = parameters[index].GetCode();
        if (c != null) {
            c.SetModule(module);
            return parameters[index].GetInput();
        }
        return null;
    }
}
