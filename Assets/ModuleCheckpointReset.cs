using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleCheckpointReset : CheckpointReset
{
    //bool activateButton = false;
    CodeModule module;
    //CodeModule copyModule = null;

    protected override void Awake()
    {
        module = GetComponent<CodeModule>();
        if (!saveOnStart)
        {
            module.editor.window.SetActive(false);
            module.editor.button.SetActive(false);
        }
        base.Awake();
    }

    protected override void SaveToCheckpoint(int _)
    {
        if (!gameObject.activeInHierarchy)
        {
            //copyModule = copy.GetComponent<CodeModule>();
            //activateButton = copyModule.editor.button.activeInHierarchy;
            Destroy(module.editor.window);
            Destroy(module.editor.button);
        }
        base.SaveToCheckpoint(_);
    }

    protected override void ResetToCheckpoint()
    {
        /*if (copyModule != null)
        {
            *//*if (TryGetComponent(out CodeModule module))
            {
                Destroy(module.editor.window);
                Destroy(module.editor.button);
            }*//*
            copyModule.editor.window.SetActive(true);
            copyModule.editor.button.SetActive(activateButton);
        }*/
        if (gameObject.activeInHierarchy)
        {
            Destroy(module.editor.window);
            Destroy(module.editor.button);
        }
        else
        {
            module.editor.window.SetActive(true);
            module.editor.button.SetActive(module.editableOnStart);
        }
        base.ResetToCheckpoint();
    }
}
