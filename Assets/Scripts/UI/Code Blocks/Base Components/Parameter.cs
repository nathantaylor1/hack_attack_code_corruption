using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parameter : InputField
{
    // Used to distinguish the Parameter Input fields

    public override void AddInputBlock(Code _inputBlock)
    {
        if (inputBlock) {
            inputBlock.GetComponent<CanvasGroup>().blocksRaycasts = true;
            InventoryManager.instance.AddBlock(inputBlock);
        }
        inputBlock = _inputBlock;
        //inputBlock.SetBodyParent(parentBlock.GetBodyParent());
        Deselect();
    }

    public override bool CanAcceptInput(string returnType)
    {
        return inputTypeSet.Contains(returnType.ToLowerInvariant());
    }

    public override void Select()
    {
        if (inputBlock != null) {
            Image temp = inputBlock.GetComponentInChildren<Image>();
            Color tempc = temp.color;
            tempc.a = .75f;
            temp.color = tempc;
        }
        base.Select();
    }

    public override void Deselect()
    {
        Debug.Log(inputBlock);
        if (inputBlock != null) {
            Image temp = inputBlock.GetComponentInChildren<Image>();
            Color tempc = temp.color;
            tempc.a = 1;
            temp.color = tempc;
        }
        base.Deselect();
    }
}
