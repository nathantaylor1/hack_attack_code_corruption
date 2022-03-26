using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTime : MonoBehaviour
{
    [SerializeField] private float time = 3.0f;

    private void Awake()
    {
        StartCoroutine(DestroyAfterTime());
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
        yield return null;
    }
}
