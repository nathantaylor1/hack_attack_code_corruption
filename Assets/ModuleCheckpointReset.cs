using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleCheckpointReset : CheckpointReset
{
    bool activateButton = false;
    CodeModule copyModule = null;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void SaveToCheckpoint(int _)
    {
        base.SaveToCheckpoint(_);
        SaveEditor();
    }

    protected virtual void SaveEditor()
    {
        copyModule = copy.GetComponent<CodeModule>();
        activateButton = copyModule.editor.button.activeInHierarchy;
        /*copyModule.editor.window = Instantiate(module.editor.window);
        copyModule.editor.button = Instantiate(module.editor.button);*/

        //copyModule.InitEditor();
        /*copyModule.editor.window.SetActive(false);
        copyModule.editor.button.SetActive(false);*/
    }

    protected override void ResetToCheckpoint()
    {
        if (copyModule != null)
        {
            if (TryGetComponent(out CodeModule module))
            {
                Destroy(module.editor.window);
                Destroy(module.editor.button);
            }
            copyModule.editor.window.SetActive(true);
            copyModule.editor.button.SetActive(activateButton);
        }
        base.ResetToCheckpoint();
    }
}
