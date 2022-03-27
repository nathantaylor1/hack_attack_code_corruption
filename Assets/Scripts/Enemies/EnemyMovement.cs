using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyMovement : MonoBehaviour
{
    public SpriteMask healthMask;
    protected void FlipDirection() {
        healthMask.transform.localScale = new Vector3(healthMask.transform.localScale.x * -1, 1, 1);
    }
}