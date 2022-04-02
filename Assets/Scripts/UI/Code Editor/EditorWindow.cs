using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorWindow : MonoBehaviour
{
    [SerializeField]
    protected Transform codeSpaces;
    private int id = -1;
    //protected CanvasGroup cg;

    private void Awake()
    {
        //cg = GetComponent<CanvasGroup>();
    }
    public int GetID() {
        return id;
    }

    public void SetCodeSpaceModules(CodeModule module, int _id)
    {
        id = _id;
        foreach (Transform child in codeSpaces)
        {
            CodeSpace cs = child.GetComponentInChildren<CodeSpace>();
            if (cs != null)
            {
                cs.SetModule(module);
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
