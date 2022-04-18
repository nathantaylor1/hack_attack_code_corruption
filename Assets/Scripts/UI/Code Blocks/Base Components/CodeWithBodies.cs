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

    public override void ExecuteSecondaryCode()
    {
        if (module != null)
        {
            foreach (InputField c in bodies)
            {
                if (c.GetCode() != null && module!)
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
            foreach (InputField c in bodies)
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

    protected virtual Code GetBody(int index)
    {
        if (index < bodies.Count) {
            return bodies[index].GetCode();
        }
        return null;
    }

    public virtual void OnBodyCompletion()
    {
        //Continue();
    }
}
