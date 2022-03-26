using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnedSpace : CodeSpace
{
    bool collisionCheckThisFrame = false;
    bool destroyed = false;
    public void SetCode(Code _start) {
        start = _start;
    }

    public override void SetModule(CodeModule _module)
    {
        _module.OnCheckCollision.AddListener(CollisionCalled);
        base.SetModule(_module);
    }

    protected void Update()
    {
        if (!EditorController.instance.is_in_editor && !destroyed) {
                collisionCheckThisFrame = false;
                StartExecution();
                if (!collisionCheckThisFrame && CheckCollision()) {
                    Destroy(gameObject);
                    destroyed = true;
                }
                // EndExecution();
        }
    }

    public bool CheckCollision() {
        return module.col.IsTouchingLayers(~(1<<module.col.gameObject.layer));
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