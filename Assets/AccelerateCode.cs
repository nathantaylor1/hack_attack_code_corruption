using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerateCode : CodeWithParameters
{
    public override void ExecuteCode()
    {
        Vector2 direction = (Vector2)(object)GetParameter(0);
        float multiplier = (float)(object)GetParameter(1);
        module.rb.AddForce(direction * module.accelerationSpeed * multiplier, ForceMode2D.Force);
        base.ExecuteCode();
    }
}
