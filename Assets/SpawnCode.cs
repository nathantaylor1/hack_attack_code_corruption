using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCode : CodeWithBodies
{
    public GameObject thingToSpawn;
    public override void ExecuteCode()
    {
        var g = Instantiate(thingToSpawn, module.transform.position, Quaternion.identity);
        GetBody(0).SetModule(g.GetComponent<CodeModule>());
        GetBody(0).ExecuteCode();
        base.ExecuteCode();
    }
}
