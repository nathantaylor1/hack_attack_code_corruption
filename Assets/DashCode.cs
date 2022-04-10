using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashCode : CodeWithParameters
{
    bool isDashing = false;
    public override void ExecuteCode()
    {
        if (!isDashing) {
            var p0 = GetParameter(0);
            var p1 = GetParameter(1);
            var p2 = GetParameter(2);
            if (!(p0 is null) && !(p1 is null) && !(p2 is null)) {
                Vector2 direction = ((Vector2)(object)p0).normalized;
                float moveSpeed = (float)(object)p1;
                float paramTime = ((float)(object)p2);
                float reloadTime = module.dashDelay / paramTime;
                StartCoroutine(Dash(direction, moveSpeed, reloadTime));
            }
        }
        base.ExecuteCode();
    }

    IEnumerator Dash(Vector2 direction, float moveSpeed, float reloadTime)
    {
        module.anim.SetTrigger("Dash");
        isDashing = true;
        module.rb.velocity = direction * moveSpeed * module.dashSpeed;
        yield return new WaitForSeconds(module.dashDuration);
        module.rb.velocity = Vector3.zero;
        module.anim.SetTrigger("Idle");
        yield return new WaitForSeconds(reloadTime);
        //print("ready to dash");
        isDashing = false;
    }
}
