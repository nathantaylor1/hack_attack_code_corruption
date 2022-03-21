using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorWindow : MonoBehaviour
{
    [SerializeField]
    protected Transform codeSpaces;

    public void SetCodeSpaceModules(CodeModule module)
    {
        foreach (Transform child in codeSpaces)
        {
            CodeSpace cs = child.GetComponentInChildren<CodeSpace>();
            if (cs != null)
            {
                cs.SetModule(module);
            }
        }
    }
}
