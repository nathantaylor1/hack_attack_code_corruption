using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InputField : MonoBehaviour
{
    [System.Serializable]
    public class BlockImage
    {
        public Sprite selectedImage;
        public Sprite unselectedImage;
        public Image image;
        [HideInInspector]
        public Color color;
    }
    [SerializeField]
    protected List<BlockImage> blockImages = new List<BlockImage>();
    //protected Color color;

    [SerializeField]
    protected List<string> inputTypes;
    protected HashSet<string> inputTypeSet;
    protected dynamic inputVal;
    protected Code inputBlock = null;
    protected Code parentBlock = null;
    //protected Code parentBlock = null;
    public UnityEvent droppedInto = new UnityEvent();

    protected virtual void Awake()
    {
        //inputFieldImage = GetComponentInChildren<Image>();
        //inputFieldImage.sprite = unselectedImage;
        foreach (BlockImage bi in blockImages)
        {
            bi.color = bi.image.color;
        }
        Deselect();
        inputTypes.ConvertAll(s => s.ToLowerInvariant());
        inputTypeSet = new HashSet<string>(inputTypes);
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<Code>(out Code cd)) {
                AddInputBlock(cd);
                break;
            }
        }

        parentBlock = GetComponentInParent<Code>();
        // Debug.Log("inputting");
    }

    public virtual bool CanAcceptInput(string returnType)
    {
        return inputBlock == null && inputTypeSet.Contains(returnType.ToLowerInvariant());
    }

    public virtual void Select()
    {
        //inputFieldImage.sprite = selectedImage;
        foreach (BlockImage bi in blockImages)
        {
            bi.image.sprite = bi.selectedImage;
            bi.image.color = Color.white;
        }
    }

    public virtual void Deselect()
    {
        //inputFieldImage.sprite = unselectedImage;
        foreach (BlockImage bi in blockImages)
        {
            bi.image.sprite = bi.unselectedImage;
            if (bi.unselectedImage == null)
            {
                bi.image.color = Color.clear;
            }
            else
            {
                bi.image.color = bi.color;
            }
        }
    }

    public virtual void AddInputBlock(Code _inputBlock)
    {
        droppedInto.Invoke();
        inputBlock = _inputBlock;
        /*if (parentBlock != null)
        {
            //inputBlock.SetModuleRecursive(parentBlock.GetModule());
            *//*if (inputBlock is TowardsCode)
            {
                (inputBlock as TowardsCode).CheckIsActive();
            }*//*
        }*/
        //inputBlock.SetBodyParent(parentBlock.GetBodyParent());
        Deselect();
    }

    public virtual void RemoveInputBlock()
    {
        //inputBlock.StopExecution();
        inputBlock.StopSecondaryExecution();
        inputBlock = null;
    }

    public virtual dynamic GetInput()
    {
        return inputBlock.ReturnValue();
    }

    public virtual Code GetCode()
    {
        return inputBlock;
    }
}
