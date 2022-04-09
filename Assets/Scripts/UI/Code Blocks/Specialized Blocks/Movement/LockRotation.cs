using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LockRotation : MonoBehaviour
{
    public float lockedZ;
    private void LateUpdate() {
        transform.rotation = Quaternion.identity;
        transform.position = new Vector3(transform.position.x, transform.position.y, lockedZ);
    }

}