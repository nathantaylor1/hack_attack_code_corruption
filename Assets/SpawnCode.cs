using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCode : CodeWithBodies
{
    public GameObject thingToSpawn;
    private int delay = 20;
    private int elapsed = 20;
    
    public override void ExecuteCode()
    {
        // Adding delay to prevent too many spawns
        elapsed += 1;
        if (elapsed > delay) {
            elapsed = 0;
            if (module.shootSound != null && AudioManager.instance != null)
                AudioManager.instance.PlaySound(module.shootSound, module.transform.position);
            var g = Instantiate(thingToSpawn, module.shootFrom.transform.position, Quaternion.identity);
            g.SetActive(true);
            var ss = g.AddComponent<SpawnedSpace>();
            ss.SetModule(g.GetComponent<CodeModule>());
            ss.SetCode(GetBody(0));
        }
        base.ExecuteCode();
    }

    public override void StopExecution()
    {
        elapsed = delay;
        base.StopExecution();
    }
}
