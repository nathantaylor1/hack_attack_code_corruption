using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCode : CodeWithBodies
{
    protected bool canShoot = true;
	
    public override void ExecuteCode()
    {
        
        if (canShoot)
        {
            var p0 = GetParameter(0);
            var p1 = GetParameter(1);
            var p2 = GetParameter(2);
            var p3 = GetParameter(3);
            if (!(p0 is null) && !(p1 is null) && !(p2 is null) && !(p3 is null) && (float)(object)p3 != 0) {
                // start reload here in case the stuff below errors
                StartCoroutine(ReloadCoroutine((float)(object) p3));
                //AudioManager.instance.PlaySound(module.shootSound, module.transform.position);
                var g = Instantiate((GameObject)(object)p0, module.shootFrom.transform.position, Quaternion.identity);
                g.SetActive(true);
                var ss = g.AddComponent<SpawnedSpace>();
                var b = g.GetComponent<CodeModule>();
                b.father = module.gameObject;
                ss.SetModule(b);
                ss.SetParentLayer(module.gameObject.layer);
                ss.SetCode(GetBody(0));
                
                if (g.TryGetComponent(out Rigidbody2D grb))
                {
                    grb.velocity = ((Vector2)(object)p1).normalized * (float)(object)p2 * (b.moveSpeed + module.projectileSpeed);
                }
            }
        }
        base.ExecuteCode();
    }

    protected IEnumerator ReloadCoroutine(float p3)
    {
        canShoot = false;
        yield return new WaitForSeconds(1f / p3);
        canShoot = true;
    }
}