using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyInputField : InputField
{
    public override void AddInputBlock(Code _inputBlock)
    {
        inputBlock = _inputBlock;
        inputBlock.SetBodyParent(this);
        Deselect();
    }

    /*public override void SignalCompletion()
    {
        if (transform.parent.TryGetComponent(out Code prevBlock))
        {
            prevBlock.SignalCompletion();
        }
        else if (transform.parent.TryGetComponent(out InputField parentField))
        {
            parentField.SignalCompletion();
        }
    }*/
}
