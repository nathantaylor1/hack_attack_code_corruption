using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCode : CodeWithBodies
{
    public GameObject thingToSpawn;
    protected bool canShoot = true;
    public override void ExecuteCode()
    {
        if (canShoot)
        {
            var g = Instantiate(thingToSpawn, module.shootFrom.transform.position, Quaternion.identity);
            g.SetActive(true);
            if (g.TryGetComponent(out Rigidbody grb))
            {
                grb.velocity = ((Vector2)(object)GetParameter(0)).normalized * (float)(object)GetParameter(1);
            }
            var ss = g.AddComponent<SpawnedSpace>();
            ss.SetModule(g.GetComponent<CodeModule>());
            ss.SetCode(GetBody(0));
            StartCoroutine(ReloadCoroutine());
        }
        base.ExecuteCode();
    }

    protected IEnumerator ReloadCoroutine()
    {
        canShoot = false;
        yield return new WaitForSeconds(module.reloadTime);
        canShoot = true;
    }
}
