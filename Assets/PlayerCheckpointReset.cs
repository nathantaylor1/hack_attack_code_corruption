using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpointReset : CheckpointReset
{
    protected Vector3 positionCopy;
    protected Rigidbody2D rb;
    protected Rigidbody2D rbCopy;
    protected GameObject windowCopy;
    protected CodeModule module;

    protected override void Awake()
    {
        shouldSave = true;
        rb = GetComponent<Rigidbody2D>();
        module = GetComponent<CodeModule>();
        base.Awake();
    }

    protected override void SaveToCheckpoint(int _)
    {
        positionCopy = transform.position;
        rbCopy = Copy.Component<Rigidbody2D>(rb, GameManager.instance.gameObject);
        if (windowCopy != null)
        {
            Destroy(windowCopy);
        }
        windowCopy = EditorController.instance.CreateLoneWindow(module.editor.window);
        windowCopy.SetActive(false);
    }

    protected override void ResetToCheckpoint()
    {
        transform.parent = null;
        transform.position = positionCopy;
        rb = Copy.Component<Rigidbody2D>(rbCopy, gameObject);
        GameObject tempWindow = module.editor.window;
        //Destroy(module.editor.window);
        //Debug.Log(windowCopy.name);
        module.editor = EditorController.instance.AddWindowCopyless(windowCopy, module.editor.button, module);
        module.editor.window.SetActive(true);
        module.editor.button.SetActive(true);
        windowCopy = null;
        Destroy(tempWindow);
        SaveToCheckpoint(0);
    }
}
