using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnedSpace : CodeSpace
{
    protected int parentLayer = 0;
    bool collisionCheckThisFrame = false;
    bool destroyed = false;
    public void SetCode(Code _start) {
        start = _start;
    }

    public void SetParentLayer(int _parentLayer)
    {
        parentLayer = _parentLayer;
    }

    public override void SetModule(CodeModule _module)
    {
        _module.OnCheckCollision.AddListener(CollisionCalled);
        base.SetModule(_module);
    }

    protected void FixedUpdate()
    {
        if (!EditorController.instance.is_in_editor && !destroyed) {
                collisionCheckThisFrame = false;
                StartExecution();
                if (!collisionCheckThisFrame && CheckCollision()) {
                    // StartCoroutine(waitOnePass());
                    Destroy(gameObject);
                    destroyed = true;
                }
                // EndExecution();
        }
    }

    public bool CheckCollision() {
        bool temp = true;
        if (module.father != null && module.father.GetComponent<Collider2D>() != null) {
            temp = !module.col.IsTouching(module.father.GetComponent<Collider2D>());
        }
        return module.col.IsTouchingLayers(~(1<<module.col.gameObject.layer)) && temp;
    }

    public void CollisionCalled() {
        collisionCheckThisFrame = true;
    }
    // public Code GetCode() {
    //     //get Start block
    //     foreach (Transform child in transform)
    //     {
    //         return child.GetComponent<StartCode>();
    //     }
    //     return null;
    // }
}