using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBlock : CodeWithParameters
{
    public ContactFilter2D cf = new ContactFilter2D().NoFilter();
    // Check a maximum of 20 collisions
    private Collider2D[] cols = new Collider2D[20];
    protected Collider2D col = null;
    protected bool listening = false;
    protected CodeModule previousModule = null;
    public override void ExecuteCode()
    {
        // need to update this in the case of the code block is dragged to a different module 
        if (previousModule == null) {
            previousModule = module;
        }
        if  (previousModule != module) {
            previousModule.collided.RemoveListener(checkCollision);
            previousModule = module;
            listening = false;
        }
        if (!listening) {
            listening = true;
            module.collided.AddListener(checkCollision);
        }
        Debug.Log("running");

        if (col != null) {
            float dmg = (float)(object)GetParameter(0);
            // Debug.Log("damaging" + dmg + "with k:" + k);
            int moduleId = module.gameObject.GetInstanceID();
            int daddyId = module.father.GetInstanceID();
                // Debug.Log(cols[i].name);
            if (col.gameObject.GetInstanceID() != moduleId && col.gameObject.GetInstanceID() != daddyId ) {
                if (col.TryGetComponent<HasHealth>(out HasHealth h)) {
                    h.Damage(dmg);
                    // Debug.Log("damaging player");
                }
                // Used to get the health of the printer
                if (col.transform.parent != null && col.transform.parent.TryGetComponent<HasHealth>(out HasHealth h1)) {
                    h1.Damage(dmg);
                    // Debug.Log("damaging printer");
                }
            }
            col = null;
        }
        // for (int i = 0; i < k; i++)
        // {
        //     // Debug.Log(cols[i].name);
        //     if (cols[i].gameObject.GetInstanceID() != moduleId && cols[i].gameObject.GetInstanceID() != daddyId ) {
        //         if (cols[i].TryGetComponent<HasHealth>(out HasHealth h)) {
        //             h.Damage(dmg);
        //             // Debug.Log("damaging player");
        //         }
        //         // Used to get the health of the printer
        //         if (cols[i].transform.parent != null && cols[i].transform.parent.TryGetComponent<HasHealth>(out HasHealth h1)) {
        //             h1.Damage(dmg);
        //             // Debug.Log("damaging printer");
        //         }
        //     }
        // }
        base.ExecuteCode();
    }

    public override void StopExecution()
    {
        col = null;
        listening = false;
        if (module != null) {
            module.collided.RemoveListener(checkCollision);
        }
        base.StopExecution();
    }

    void checkCollision(Collision2D other) {
        col = other.collider;
    }
}