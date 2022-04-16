using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleCheckpointReset : CheckpointReset
{
    CodeModule module;

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

    protected override void SaveToCheckpoint(Transform checkpoint)//int _)
    {
        if (shouldSave && !gameObject.activeInHierarchy)
        {
            Destroy(module.editor.window);
            Destroy(module.editor.button);
        }
        base.SaveToCheckpoint(checkpoint);
    }

    protected override void ResetToCheckpoint()
    {
        if (shouldReset/* && hasSaved*/)
        {
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
        }
        base.ResetToCheckpoint();
    }
}
