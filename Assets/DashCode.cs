using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashCode : CodeWithParameters
{
    bool isDashing = false;
    public override void ExecuteCode()
    {
        if (isDashing) return;
        StartCoroutine(Dash());
        base.ExecuteCode();
    }

    IEnumerator Dash()
    {
        module.anim.SetTrigger("Dash");
        isDashing = true;
        Vector2 direction = (Vector2)(object)GetParameter(0);
        float moveSpeed = (float)(object)GetParameter(1);
        float reloadTime = module.dashDelay / ((float)(object)GetParameter(2));
        module.rb.velocity = direction * moveSpeed * module.dashSpeed;
        yield return new WaitForSeconds(module.dashDuration);
        module.rb.velocity = Vector3.zero;
        module.anim.SetTrigger("Idle");
        yield return new WaitForSeconds(reloadTime);
        //print("ready to dash");
        isDashing = false;
    }
}
