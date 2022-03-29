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
            //AudioManager.instance.PlaySound(module.shootSound, module.transform.position);
            var g = Instantiate(thingToSpawn, module.shootFrom.transform.position, Quaternion.identity);
            g.SetActive(true);
            var ss = g.AddComponent<SpawnedSpace>();
            var b = g.GetComponent<CodeModule>();
            if (g.TryGetComponent(out Rigidbody2D grb))
            {
                grb.velocity = ((Vector2)(object)GetParameter(0)).normalized * (float)(object)GetParameter(1) * b.moveSpeed;
            }
            b.father = module.gameObject;
            ss.SetModule(b);
            ss.SetParentLayer(module.gameObject.layer);
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
