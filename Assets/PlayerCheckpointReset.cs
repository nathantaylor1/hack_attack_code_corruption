using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpointReset : CheckpointReset
{
    /*protected HasHealth health;
    protected HasHealth healthCopy;*/
    protected Vector3 positionCopy;
    protected Rigidbody2D rb;
    protected Rigidbody2D rbCopy;
    //protected GameObject window;
    protected GameObject windowCopy;
    //protected GameObject button;
    protected CodeModule module;

    protected override void Awake()
    {
        //health = GetComponent<HasHealth>();
        rb = GetComponent<Rigidbody2D>();
        module = GetComponent<CodeModule>();
        base.Awake();
    }

    protected override void SaveToCheckpoint(int _)
    {
        //healthCopy = Copy.Component<HasHealth>(health, GameManager.instance.gameObject);
        positionCopy = transform.position;
        rbCopy = Copy.Component<Rigidbody2D>(rb, GameManager.instance.gameObject);
        if (windowCopy != null)
        {
            Destroy(windowCopy);
        }
        windowCopy = EditorController.instance.CreateLoneWindow(module.editor.window);
        /*Debug.Log("window.name: " + module.editor.window.name);
        Debug.Log("windowCopy.name: " + windowCopy.name);*/
        windowCopy.SetActive(false);
        /*Debug.Log("Saving: " + windowCopy.name);
        Debug.Log("\tIs saved window enabled? " + windowCopy.activeInHierarchy);*/
    }

    protected override void ResetToCheckpoint()
    {
        //Debug.Log("ResetToCheckpoint called");
        //health = Copy.Component<HasHealth>(healthCopy, gameObject);
        transform.position = positionCopy;
        rb = Copy.Component<Rigidbody2D>(rbCopy, gameObject);
        Destroy(module.editor.window);
        //windowCopy.SetActive(true);
        module.editor = EditorController.instance.AddWindowCopyless(windowCopy, module.editor.button, module);
        module.editor.window.SetActive(true);
        module.editor.button.SetActive(true);
        windowCopy = null;
        /*Debug.Log("Loading: " + module.editor.window.name);
        Debug.Log("\tIs loaded window enabled? " + module.editor.window.activeInHierarchy);*/
    }
}
