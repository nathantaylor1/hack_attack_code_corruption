using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCode : CodeWithBodies
{
    protected bool canShoot = true;
	
    public override void ExecuteCode()
    {
        if (canShoot && (float)(object)GetParameter(3) != 0)
        {
            // start reload here in case the stuff below errors
            StartCoroutine(ReloadCoroutine());
            //AudioManager.instance.PlaySound(module.shootSound, module.transform.position);
            var g = Instantiate((GameObject)(object)GetParameter(0), module.shootFrom.transform.position, Quaternion.identity);
            g.SetActive(true);
            var ss = g.AddComponent<SpawnedSpace>();
            var b = g.GetComponent<CodeModule>();
            b.father = module.gameObject;
            ss.SetModule(b);
            ss.SetParentLayer(module.gameObject.layer);
            ss.SetCode(GetBody(0));
            
            if (g.TryGetComponent(out Rigidbody2D grb))
            {
                grb.velocity = ((Vector2)(object)GetParameter(1)).normalized * (float)(object)GetParameter(2) * (b.moveSpeed + module.projectileSpeed);
            }
        }
        base.ExecuteCode();
    }

    protected IEnumerator ReloadCoroutine()
    {
        canShoot = false;
        yield return new WaitForSeconds(1f / (float)(object)GetParameter(3));
        canShoot = true;
    }
}