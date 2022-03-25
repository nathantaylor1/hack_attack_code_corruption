using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputField : MonoBehaviour
{
    [SerializeField]
    protected Sprite unselectedImage;
    [SerializeField]
    protected Sprite selectedImage;
    protected Image inputFieldImage;

    [SerializeField]
    protected List<string> inputTypes;
    protected HashSet<string> inputTypeSet;
    protected dynamic inputVal;
    protected Code inputBlock = null;
    //protected Code parentBlock = null;

    protected virtual void Awake()
    {
        inputFieldImage = GetComponentInChildren<Image>();
        inputFieldImage.sprite = unselectedImage;
        inputTypes.ConvertAll(s => s.ToLowerInvariant());
        inputTypeSet = new HashSet<string>(inputTypes);
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<Code>(out Code cd)) {
                AddInputBlock(cd);
                break;
            }
        }
        // Debug.Log("inputting");
    }

    public virtual bool CanAcceptInput(string returnType)
    {
        return inputBlock == null && inputTypeSet.Contains(returnType.ToLowerInvariant());
    }

    public virtual void Select()
    {
        inputFieldImage.sprite = selectedImage;
    }

    public virtual void Deselect()
    {
        inputFieldImage.sprite = unselectedImage;
    }

    public virtual void AddInputBlock(Code _inputBlock)
    {
        inputBlock = _inputBlock;
        //inputBlock.SetBodyParent(parentBlock.GetBodyParent());
        Deselect();
    }

    public virtual void RemoveInputBlock()
    {
        inputBlock = null;
    }

    public virtual dynamic GetInput()
    {
        return inputBlock.ReturnValue();
    }

    public virtual dynamic GetInput(CodeModule module)
    {
        inputBlock.SetModule(module);
        return inputBlock.ReturnValue();
    }
    

    public virtual Code GetCode()
    {
        return inputBlock;
    }
}
