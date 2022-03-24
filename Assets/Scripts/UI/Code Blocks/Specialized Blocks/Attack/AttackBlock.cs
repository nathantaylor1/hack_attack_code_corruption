using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBlock : CodeWithParameters
{
    //protected List<InputField> parameters = new List<InputField>();
    
    protected GameObject prefab = null;
    protected float cooldownTime = 1.0f;
    protected bool canAttack = true;
    protected float dmg = 0.0f;

    protected IEnumerator CO_Cooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(cooldownTime);
        canAttack = true;
        yield return null;
    }
}
