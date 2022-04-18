using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpointReset : CheckpointReset
{
    protected Rigidbody2D rb;
    //protected Rigidbody2D rbCopy;
    [SerializeField]
    protected float gravCopy = 0f;
    protected GameObject windowCopy;
    protected CodeModule module;
    protected HasHealth health;

    protected override void Awake()
    {
        shouldSave = true;
        rb = GetComponent<Rigidbody2D>();
        module = GetComponent<CodeModule>();
        health = GetComponent<HasHealth>();
        base.Awake();
    }

    protected override void SaveToCheckpoint(Transform checkpoint)//int _)
    {
        posCopy = checkpoint.position;
        //rbCopy = Copy.Component<Rigidbody2D>(rb, GameManager.instance.gameObject);
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
        transform.position = posCopy;
        transform.rotation = Quaternion.identity;
        //rb = Copy.Component<Rigidbody2D>(rbCopy, gameObject);
        rb.velocity = Vector2.zero;
        rb.gravityScale = module.gravityScale;
        health.isDead = false;
        GameObject tempWindow = module.editor.window;
        //Destroy(module.editor.window);
        Debug.Log(windowCopy.name);
        module.editor = EditorController.instance.AddWindowCopyless(windowCopy, module.editor.button, module);
        module.editor.window.SetActive(true);
        module.editor.button.SetActive(true);
        windowCopy = null;
        Destroy(tempWindow);
        SaveToCheckpoint(transform);
    }
}
