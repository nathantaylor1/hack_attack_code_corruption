using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeWithParameters : Code
{
    protected List<InputField> parameters = new List<InputField>();

    protected override void Awake()
    {
        base.Awake();
        Transform signature = transform.Find("Background").Find("Signature");
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

    protected virtual dynamic GetParameter(int index)
    {
        return parameters[index].GetInput();
    }
}
