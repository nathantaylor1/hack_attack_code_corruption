using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorWindow : MonoBehaviour
{
    [SerializeField]
    protected Transform codeSpaces;
    protected CodeModule module;
    //protected CanvasGroup cg;

    private void Awake()
    {
        //cg = GetComponent<CanvasGroup>();
    }

    public CodeModule GetModule()
    {
        return module;
    }

    public void SetCodeSpaceModules(CodeModule _module)
    {
        module = _module;
        foreach (Transform child in codeSpaces)
        {
            CodeSpace cs = child.GetComponentInChildren<CodeSpace>();
            if (cs != null)
            {
                cs.SetModule(_module);
            }
        }
    }

    public void ToggleEnabled(bool enabled)
    {
        foreach (Transform child in codeSpaces)
        {
            InventoryDrop id = child.GetComponentInChildren<InventoryDrop>();
            if (id != null)
            {
                id.ToggleEnabled(enabled);
            }
        }
        if (TryGetComponent(out CanvasGroup cg))
        {
            cg.alpha = enabled ? 1 : 0;
            cg.interactable = enabled;
            cg.blocksRaycasts = enabled;
        }
    }
}
