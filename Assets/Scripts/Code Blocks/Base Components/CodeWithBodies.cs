using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeWithBodies : CodeWithParameters
{
    protected List<InputField> bodies = new List<InputField>();

    protected override void Awake()
    {
        base.Awake();
        // Should only iterate through top layer
        foreach (Transform bodyTrans in transform.Find("Background").transform)
        {
            if (bodyTrans.TryGetComponent(out InputField body))
            {
                if (body.GetInstanceID() != GetInstanceID())
                    bodies.Add(body);
                else
                    nextBlockInput = body;
            }
        }
    }

    protected virtual Code GetBody(int index)
    {
        return bodies[index].GetCode();
    }

    public virtual void OnBodyCompletion()
    {
        //Continue();
    }
}
